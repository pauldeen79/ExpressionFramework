namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CompoundExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Aggregation_Of_Context_And_SecondExpression()
    {
        // Arrange
        var sut = new CompoundExpression(new ConstantExpression(2), new AddAggregator());

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2);
    }
}
