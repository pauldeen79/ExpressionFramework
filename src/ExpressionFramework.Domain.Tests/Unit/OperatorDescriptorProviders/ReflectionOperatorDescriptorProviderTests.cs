namespace ExpressionFramework.Domain.Tests.Unit.OperatorDescriptorProviders;

public class ReflectionOperatorDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionOperatorDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.UsesLeftValue.Should().BeFalse();
        actual.UsesRightValue.Should().BeFalse();
        actual.LeftValueTypeName.Should().BeNull();
        actual.RightValueTypeName.Should().BeNull();
    }
}
