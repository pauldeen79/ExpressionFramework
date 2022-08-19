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
        var actual = CreateExpressionEvaluator().Evaluate(context, expression);

        // Assert
        actual.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_ConstantExpression()
    {
        // Arrange
        var expression = new ConstantExpressionBuilder().WithValue("Hello world").Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(null, expression);

        // Assert
        actual.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_DelegateExpression()
    {
        // Arrange
        var expression = new DelegateExpressionBuilder().WithValueDelegate((context, _, _) => context?.GetType()?.GetProperty("Name")?.GetValue(context)).Build();
        var context = new { Name = "Hello world" };

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(context, expression);

        // Assert
        actual.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_EmptyExpression()
    {
        // Arrange
        var expression = new EmptyExpressionBuilder().Build();

        // Act
        var actual = CreateExpressionEvaluator().Evaluate(null, expression);

        // Assert
        actual.Should().BeNull();
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
            .WithLeftExpression(new DelegateExpressionBuilder().WithValueDelegate((_, _, _) => "12345"))
            .WithOperator(Operator.Equal)
            .WithRightExpression(new DelegateExpressionBuilder().WithValueDelegate((_, _, _) => "12345"))
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
