namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ContextExpressionTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ContextExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ContextExpression));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.ContextIsRequired.ShouldBe(true);
    }
}
