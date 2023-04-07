namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CompoundExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Aggregation_Of_Context_And_SecondExpression()
    {
        // Arrange
        var sut = new CompoundExpression(new ConstantExpression(1), new ConstantExpression(2), new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new CompoundExpressionBase(new EmptyExpression(), new EmptyExpression(), new AddAggregator());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
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
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
