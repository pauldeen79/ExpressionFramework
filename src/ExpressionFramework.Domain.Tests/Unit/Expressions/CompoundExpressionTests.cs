namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CompoundExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Aggregation_Of_FirstExpression_And_SecondExpression_Using_Constant_Values()
    {
        // Arrange
        var sut = new CompoundExpressionBuilder()
            .WithFirstExpression(1)
            .WithSecondExpression(2)
            .WithAggregator(new AddAggregatorBuilder())
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(CompoundExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(CompoundExpression));
        result.Parameters.Should().HaveCount(4);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
