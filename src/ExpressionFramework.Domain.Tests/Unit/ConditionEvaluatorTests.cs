namespace ExpressionFramework.Domain.Tests.Unit;

public sealed class ConditionEvaluatorTests : IDisposable
{
    private IConditionEvaluator CreateSut() => _provider.GetRequiredService<IConditionEvaluator>();
    private readonly ServiceProvider _provider;

    public ConditionEvaluatorTests()
    {
        _provider = new ServiceCollection().AddExpressionFramework().BuildServiceProvider();
    }

    [Fact]
    public async Task Evaluate_Works_Correctly_On_Equals_With_Sequences()
    {
        // Arrange
        var condition = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" })))
            .WithRightExpression(new ConstantExpressionBuilder().WithValue(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" })))
            .WithOperator(OperatorBuilderFactory.Create(new EqualsOperator()))
            .Build();

        // Act
        var actual = await CreateSut().Evaluate(null, new[] { condition });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public async Task Evaluate_Works_Correctly_On_Contains_With_Sequence_Of_Strings()
    {
        // Arrange
        var condition = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(new[] { "1", "2", "3" }))
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("2" ))
            .WithOperator(OperatorBuilderFactory.Create(new ContainsOperator()))
            .Build();

        // Act
        var actual = await CreateSut().Evaluate(null, new[] { condition });


        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public async Task Can_Evaluate_Multiple_Conditions_With_And_Combination()
    {
        // Arrange
        var sut = CreateSut();
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = await sut.Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public async Task Can_Evaluate_Multiple_Conditions_With_Or_Combination()
    {
        // Arrange
        var sut = CreateSut();
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();

        // Act
        var actual = await sut.Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public async Task Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_1()
    {
        // Arrange
        var sut = CreateSut();
        //This translates to: True&(False|True) -> True
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithStartGroup()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();
        var condition3 = new ConditionBuilder()
            .WithEndGroup()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = await sut.Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public async Task Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_2()
    {
        // Arrange
        var sut = CreateSut();
        //This translates to: False|(True&True) -> True
        var condition1 = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("wrong"))
            .Build();
        var condition2 = new ConditionBuilder()
            .WithStartGroup()
            .WithCombination(Combination.Or)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();
        var condition3 = new ConditionBuilder()
            .WithEndGroup()
            .WithCombination(Combination.And)
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .WithOperator(new EqualsOperatorBuilder())
            .WithRightExpression(new ConstantExpressionBuilder().WithValue("54321"))
            .Build();

        // Act
        var actual = await sut.Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    public void Dispose()
        => _provider.Dispose();
}
