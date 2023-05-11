namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedContextExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_When_Context_Is_Of_Right_Type()
    {
        // Arrange
        var expression = new TypedContextExpression<int>();

        // Act
        var result = expression.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Of_Wrong_Type()
    {
        // Arrange
        var expression = new TypedContextExpression<int>();

        // Act
        var result = expression.Evaluate("no integer");

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type System.Int32");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new TypedContextExpressionBase<int>();

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedContextExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TypedContextExpression<int>));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
