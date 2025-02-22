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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Of_Wrong_Type()
    {
        // Arrange
        var expression = new TypedContextExpression<int>();

        // Act
        var result = expression.Evaluate("no integer");

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Context is not of type System.Int32");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedContextExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TypedContextExpression<int>));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.ContextIsRequired.ShouldBe(true);
    }
}
