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
