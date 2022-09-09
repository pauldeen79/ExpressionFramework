using CrossCutting.Common;

namespace ExpressionFramework.Domain.Tests.Unit;

public sealed class ConditionEvaluatorTests : IDisposable
{
    private IConditionEvaluator CreateSut() => _provider.GetRequiredService<IConditionEvaluator>();
    private readonly ServiceProvider _provider;

    public ConditionEvaluatorTests()
    {
        _provider = new ServiceCollection().AddExpressionFramework().BuildServiceProvider();
    }

    [Theory]
    [InlineData("A", "A", typeof(EqualsOperator), true)]
    [InlineData("A", "a", typeof(EqualsOperator), true)]
    [InlineData("A", "b", typeof(EqualsOperator), false)]
    [InlineData(null, "b", typeof(EqualsOperator), false)]
    [InlineData("A", null, typeof(EqualsOperator), false)]
    [InlineData("", "", typeof(EqualsOperator), true)]
    [InlineData(null, null, typeof(EqualsOperator), true)]
    [InlineData(true, true, typeof(EqualsOperator), true)]
    [InlineData(true, false, typeof(EqualsOperator), false)]
    [InlineData(1, 1, typeof(EqualsOperator), true)]
    [InlineData(1, 2, typeof(EqualsOperator), false)]
    [InlineData(true, 1, typeof(EqualsOperator), false)]
    [InlineData(true, "", typeof(EqualsOperator), false)]
    [InlineData("A", "A", typeof(NotEqualsOperator), false)]
    [InlineData("A", "a", typeof(NotEqualsOperator), false)]
    [InlineData("A", "b", typeof(NotEqualsOperator), true)]
    [InlineData(null, "b", typeof(NotEqualsOperator), true)]
    [InlineData("A", null, typeof(NotEqualsOperator), true)]
    [InlineData(1, 2, typeof(IsGreaterOperator), false)]
    [InlineData(2, 2, typeof(IsGreaterOperator), false)]
    [InlineData(3, 2, typeof(IsGreaterOperator), true)]
    [InlineData(null, 2, typeof(IsGreaterOperator), false)]
    [InlineData(2, null, typeof(IsGreaterOperator), false)]
    [InlineData(1, 2, typeof(IsGreaterOrEqualOperator), false)]
    [InlineData(2, 2, typeof(IsGreaterOrEqualOperator), true)]
    [InlineData(3, 2, typeof(IsGreaterOrEqualOperator), true)]
    [InlineData(null, 2, typeof(IsGreaterOrEqualOperator), false)]
    [InlineData(1, null, typeof(IsGreaterOrEqualOperator), false)]
    [InlineData(2, 1, typeof(IsSmallerOperator), false)]
    [InlineData(2, 2, typeof(IsSmallerOperator), false)]
    [InlineData(2, 3, typeof(IsSmallerOperator), true)]
    [InlineData(null, 1, typeof(IsSmallerOperator), false)]
    [InlineData(2, null, typeof(IsSmallerOperator), false)]
    [InlineData(2, 1, typeof(IsSmallerOrEqualOperator), false)]
    [InlineData(2, 2, typeof(IsSmallerOrEqualOperator), true)]
    [InlineData(2, 3, typeof(IsSmallerOrEqualOperator), true)]
    [InlineData(null, 1, typeof(IsSmallerOrEqualOperator), false)]
    [InlineData(2, null, typeof(IsSmallerOrEqualOperator), false)]
    [InlineData("A", null, typeof(IsNullOperator), false)]
    [InlineData("", null, typeof(IsNullOperator), false)]
    [InlineData(null, null, typeof(IsNullOperator), true)]
    [InlineData("A", null, typeof(IsNotNullOperator), true)]
    [InlineData("", null, typeof(IsNotNullOperator), true)]
    [InlineData(null, null, typeof(IsNotNullOperator), false)]
    [InlineData("A", null, typeof(IsNullOrEmptyOperator), false)]
    [InlineData("", null, typeof(IsNullOrEmptyOperator), true)]
    [InlineData(" ", null, typeof(IsNullOrEmptyOperator), false)]
    [InlineData(null, null, typeof(IsNullOrEmptyOperator), true)]
    [InlineData("A", null, typeof(IsNotNullOrEmptyOperator), true)]
    [InlineData("", null, typeof(IsNotNullOrEmptyOperator), false)]
    [InlineData(" ", null, typeof(IsNotNullOrEmptyOperator), true)]
    [InlineData(null, null, typeof(IsNotNullOrEmptyOperator), false)]
    [InlineData("A", null, typeof(IsNullOrWhiteSpaceOperator), false)]
    [InlineData("", null, typeof(IsNullOrWhiteSpaceOperator), true)]
    [InlineData(" ", null, typeof(IsNullOrWhiteSpaceOperator), true)]
    [InlineData(null, null, typeof(IsNullOrWhiteSpaceOperator), true)]
    [InlineData("A", null, typeof(IsNotNullOrWhiteSpaceOperator), true)]
    [InlineData("", null, typeof(IsNotNullOrWhiteSpaceOperator), false)]
    [InlineData(" ", null, typeof(IsNotNullOrWhiteSpaceOperator), false)]
    [InlineData(null, null, typeof(IsNotNullOrWhiteSpaceOperator), false)]
    [InlineData("Pizza", "x", typeof(ContainsOperator), false)]
    [InlineData("Pizza", "a", typeof(ContainsOperator), true)]
    [InlineData("Pizza", "A", typeof(ContainsOperator), true)]
    [InlineData(null, "x", typeof(ContainsOperator), false)]
    [InlineData("Pizza", "x", typeof(NotContainsOperator), true)]
    [InlineData("Pizza", "a", typeof(NotContainsOperator), false)]
    [InlineData("Pizza", "A", typeof(NotContainsOperator), false)]
    [InlineData(null, "A", typeof(NotContainsOperator), false)]
    [InlineData("Pizza", "x", typeof(StartsWithOperator), false)]
    [InlineData("Pizza", "p", typeof(StartsWithOperator), true)]
    [InlineData("Pizza", "P", typeof(StartsWithOperator), true)]
    [InlineData(null, "x", typeof(StartsWithOperator), false)]
    [InlineData("Pizza", "x", typeof(NotStartsWithOperator), true)]
    [InlineData("Pizza", "p", typeof(NotStartsWithOperator), false)]
    [InlineData("Pizza", "P", typeof(NotStartsWithOperator), false)]
    [InlineData(null, "P", typeof(NotStartsWithOperator), false)]
    [InlineData("Pizza", "x", typeof(EndsWithOperator), false)]
    [InlineData("Pizza", "a", typeof(EndsWithOperator), true)]
    [InlineData("Pizza", "A", typeof(EndsWithOperator), true)]
    [InlineData(null, "x", typeof(EndsWithOperator), false)]
    [InlineData("Pizza", "x", typeof(NotEndsWithOperator), true)]
    [InlineData("Pizza", "a", typeof(NotEndsWithOperator), false)]
    [InlineData("Pizza", "A", typeof(NotEndsWithOperator), false)]
    [InlineData(null, "A", typeof(NotEndsWithOperator), false)]
    public async Task Evaluate_Returns_Correct_Result_On_Contains_Condition(object? leftValue,
                                                                            object? rightValue,
                                                                            Type @operator,
                                                                            bool expectedResult)
    {
        // Arrange
        var condition = new ConditionBuilder()
            .WithLeftExpression(new ConstantExpressionBuilder().WithValue(leftValue))
            .WithRightExpression(new ConstantExpressionBuilder().WithValue(rightValue))
            .WithOperator(OperatorBuilderFactory.Create(OperatorExpression.Parse(@operator.Name.Replace("Operator", string.Empty, StringComparison.InvariantCulture))))
            .Build();

        // Act
        var actual = await CreateSut().Evaluate(null, new[] { condition });

        // Assert
        actual.GetValueOrThrow().Should().Be(expectedResult);
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

    public void Dispose()
        => _provider.Dispose();
}
