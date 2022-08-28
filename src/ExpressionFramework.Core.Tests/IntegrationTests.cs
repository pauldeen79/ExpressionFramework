namespace ExpressionFramework.Core.Tests;

public sealed class IntegrationTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public IntegrationTests()
        => _serviceProvider = new ServiceCollection()
            .AddExpressionFramework()
            .BuildServiceProvider();

    [Fact]
    public void Can_Evaluate_FieldExpression()
    {
        // Arrange
        var expression = new FieldExpressionBuilder().WithFieldName("Name").Build();
        var context = new { Name = "Hello world" };

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(context, null, expression);

        // Assert
        actual.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_DuckTyping()
    {
        // Arrange
        var expression = new FieldExpressionBuilder().WithFieldName("InnerProperty.Name").Build();
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(context, null, expression);

        // Assert
        actual.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_ChainedExpression()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new FieldExpressionBuilder().WithFieldName("InnerProperty"),
                new FieldExpressionBuilder().WithFieldName("Name")
            )
            .Build();
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(context, null, expression);

        // Assert
        actual.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_ConstantExpression()
    {
        // Arrange
        var expression = new ConstantExpressionBuilder().WithValue("Hello world").Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(null, null, expression);

        // Assert
        actual.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_DelegateExpression()
    {
        // Arrange
        var expression = new DelegateExpressionBuilder().WithValueDelegate((context, _, _, _)
            => context?.GetType()?.GetProperty("Name")?.GetValue(context)).Build();
        var context = new { Name = "Hello world" };

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(context, null, expression);

        // Assert
        actual.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_EmptyExpression()
    {
        // Arrange
        var expression = new EmptyExpressionBuilder().Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(null, null, expression);

        // Assert
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Evaluate_AggregateExpression_With_Some_Mathematic_Functions()
    {
        /// Example: 5 + (calculationModel.NumberOfHectares / 10)
        // Arrange
        var calculationModel = new { NumberOfHectares = 50 };
        var expression = new AggregateExpressionBuilder()
            .AddExpressions
            (
                new ConstantExpressionBuilder(5),
                new AggregateExpressionBuilder()
                    .AddExpressions
                    (
                        new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares)),
                        new ConstantExpressionBuilder(10)
                    )
                    .WithAggregateFunction(new DivideAggregateFunctionBuilder())
            )
            .WithAggregateFunction(new PlusAggregateFunctionBuilder())
            .Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(calculationModel, null, expression);

        // Assert
        actual.GetValueOrThrow().Should().Be(5 + (calculationModel.NumberOfHectares / 10));
    }

    [Fact]
    public void Can_Evaluate_AggregateExpression_With_Function()
    {
        /// Example: 5 + new[] { 10 }.Length
        // Arrange
        var calculationModel = new { NumberOfHectares = 50 };
        var expression = new AggregateExpressionBuilder()
            .AddExpressions
            (
                new ConstantExpressionBuilder(5),
                new ConstantExpressionBuilder(new[] { 10 }).WithFunction(new CountFunctionBuilder())
            )
            .WithAggregateFunction(new PlusAggregateFunctionBuilder())
            .Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(calculationModel, null, expression);

        // Assert
        actual.GetValueOrThrow().Should().Be(5 + new[] { 10 }.Length);
    }

    [Fact]
    public void Can_Evaluate_AggregateExpression_With_Condition()
    {
        /// Example: new[] { 5, 5, 10 }.Where(x => x <= 5).Sum();
        // Arrange
        var expression = new AggregateExpressionBuilder()
            .AddExpressions
            (
                new ConstantExpressionBuilder(5),
                new ConstantExpressionBuilder(5),
                new ConstantExpressionBuilder(10) // this one gets ignored
            )
            .WithAggregateFunction(new PlusAggregateFunctionBuilder())
            .AddExpressionConditions(new ConditionBuilder()
                .WithLeftExpression(new ContextExpressionBuilder())
                .WithOperator(Operator.SmallerOrEqual)
                .WithRightExpression(new ConstantExpressionBuilder(5)))
            .Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(null, null, expression);

        // Assert
        actual.GetValueOrThrow().Should().Be(new[] { 5, 5, 10 }.Where(x => x <= 5).Sum());
    }

    [Fact]
    public void Can_Evaluate_AggregateExpression_With_SwitchExpression()
    {
        // Arrange
        var calculationModel = new { NumberOfHectares = 50 };
        var expression = new AggregateExpressionBuilder()
            .AddExpressions
            (
                new ConditionalExpressionBuilder()
                    .AddConditions
                    (
                        new ConditionBuilder()
                            .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                            .WithOperator(Operator.GreaterOrEqual)
                            .WithRightExpression(new ConstantExpressionBuilder(5000))
                    )
                    .WithResultExpression(new ConstantExpressionBuilder(10)),
                new ConditionalExpressionBuilder()
                    .AddConditions
                    (
                        new ConditionBuilder()
                            .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                            .WithOperator(Operator.GreaterOrEqual)
                            .WithRightExpression(new ConstantExpressionBuilder(500))
                    )
                    .WithResultExpression(new ConstantExpressionBuilder(20)),
                new ConditionalExpressionBuilder()
                    .AddConditions
                    (
                        new ConditionBuilder()
                            .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                            .WithOperator(Operator.GreaterOrEqual)
                            .WithRightExpression(new ConstantExpressionBuilder(50))
                    )
                    .WithResultExpression(new ConstantExpressionBuilder(30)), // <--------- this one gets selected, it's the first one which condition evaluates to true
                new ConditionalExpressionBuilder()
                    .AddConditions
                    (
                        new ConditionBuilder()
                            .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                            .WithOperator(Operator.GreaterOrEqual)
                            .WithRightExpression(new ConstantExpressionBuilder(5))
                    )
                    .WithResultExpression(new ConstantExpressionBuilder(40)),
                new ConstantExpressionBuilder(50), // non conditional expression will take the result (3)
                new ConstantExpressionBuilder(60)
            )
            .AddExpressionConditions(new ConditionBuilder()
                .WithLeftExpression(new ContextExpressionBuilder())
                .WithOperator(Operator.IsNotNull))
            .WithAggregateFunction(new FirstAggregateFunctionBuilder())
            .Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(calculationModel, null, expression);

        // Assert
        var expected = new List<(Func<bool> Condition, object? Result)>
        {
            new(() => false, 10),
            new(() => false, 20),
            new(() => true, 30), // <--------- this one gets selected, it's the first one which condition evaluates to true
            new(() => true, 40)
        };
        actual.GetValueOrThrow().Should().Be(expected.FirstOrDefault(x => x.Condition.Invoke()).Result ?? 50); // 30
    }

    [Fact]
    public void Can_Evaluate_SwitchExpression()
    {
        // Arrange
        var calculationModel = new { NumberOfHectares = 50 };
        var expression = new SwitchExpressionBuilder()
            .Case
            (
                new CaseBuilder().When
                (
                    new ConditionBuilder()
                        .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                        .WithOperator(Operator.GreaterOrEqual)
                        .WithRightExpression(new ConstantExpressionBuilder(5000))
                ).Then(new ConstantExpressionBuilder(10))
            )
            .Case
            (
                new CaseBuilder().When
                (
                    new ConditionBuilder()
                        .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                        .WithOperator(Operator.GreaterOrEqual)
                        .WithRightExpression(new ConstantExpressionBuilder(500))
                ).Then(new ConstantExpressionBuilder(20))
            )
            .Case
            (
                new CaseBuilder().When
                (
                    new ConditionBuilder()
                        .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                        .WithOperator(Operator.GreaterOrEqual)
                        .WithRightExpression(new ConstantExpressionBuilder(50))
                ).Then(new ConstantExpressionBuilder(30))
            )
            .Case(
                new CaseBuilder().When
                (
                    new ConditionBuilder()
                        .WithLeftExpression(new ItemExpressionBuilder().WithInnerExpression(new FieldExpressionBuilder(nameof(calculationModel.NumberOfHectares))))
                        .WithOperator(Operator.GreaterOrEqual)
                        .WithRightExpression(new ConstantExpressionBuilder(5))
                ).Then(new ConstantExpressionBuilder(40))
            )
            .Default(new ConstantExpressionBuilder(50))
            .Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(calculationModel, null, expression);

        // Assert
        var expected = new List<(Func<bool> Condition, object? Result)>
        {
            new(() => false, 10),
            new(() => false, 20),
            new(() => true, 30), // <--------- this one gets selected, it's the first one which condition evaluates to true
            new(() => true, 40)
        };
        actual.GetValueOrThrow().Should().Be(expected.FirstOrDefault(x => x.Condition.Invoke()).Result ?? 50); // 30
    }

    [Fact]
    public void Can_Evaluate_Condition_With_Constant_Expressions_True()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Condition_With_Empty_Expressions_True()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition = new ConditionBuilder()
            .WithLeftExpression(new EmptyExpressionBuilder())
            .WithOperator(Operator.Equal)
            .WithRightExpression(new EmptyExpressionBuilder())
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Condition_With_Delegate_Expressions_True()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition = new ConditionBuilder()
            .WithLeftExpression(new DelegateExpressionBuilder((_, _, _, _) => "12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new DelegateExpressionBuilder((_, _, _, _) => "12345"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Condition_With_Different_Expressions_False()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition = new ConditionBuilder()
            .WithLeftExpression(new EmptyExpressionBuilder())
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition });

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Can_Evaluate_Condition_With_Constant_Expressions_And_Functions_True()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345").WithFunction(new LeftFunctionBuilder().WithLength(1)))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345").WithFunction(new RightFunctionBuilder().WithLength(1)))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition });

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_And_Combination()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Or_Combination()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_1()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        //This translates to: True&(False|True) -> True
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithStartGroup()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();
        var condition3 = new ConditionBuilder()
            .WithEndGroup()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_2()
    {
        // Arrange
        var sut = CreateConditionEvaluator();
        //This translates to: False|(True&True) -> True
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithStartGroup()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();
        var condition3 = new ConditionBuilder()
            .WithEndGroup()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = sut.Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.Should().BeTrue();
    }

    private IConditionEvaluator CreateConditionEvaluator() => _serviceProvider.GetRequiredService<IConditionEvaluator>();

    private IExpressionEvaluator CreateExpressionEvaluator() => _serviceProvider.GetRequiredService<IExpressionEvaluator>();

    public void Dispose() => _serviceProvider.Dispose();
}
