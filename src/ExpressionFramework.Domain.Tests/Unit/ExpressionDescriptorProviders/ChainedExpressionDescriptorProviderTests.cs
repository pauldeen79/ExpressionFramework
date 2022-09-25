namespace ExpressionFramework.Domain.Tests.Unit.ExpressionDescriptorProviders;

public class ChainedExpressionDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ChainedExpressionDescriptorProvider();

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Parameters.Should().HaveCount(1);
        result.ReturnValues.Should().HaveCount(2);
    }
}
