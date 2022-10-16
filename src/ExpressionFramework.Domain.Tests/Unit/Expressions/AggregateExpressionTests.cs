namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AggregateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Aggregation_Of_Context_And_SecondExpression()
    {
        // Arrange
        var sut = new AggregateExpression(new object[] { 2, 3 }, new AddAggregator());

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2 + 3);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AggregateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(AggregateExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }
}
