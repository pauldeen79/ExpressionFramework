namespace ExpressionFramework.Domain.Tests.Unit.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionExpressionDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.UsesContext.Should().BeFalse();
        actual.ContextDescription.Should().BeNull();
        actual.ContextTypeName.Should().BeNull();
    }
}
