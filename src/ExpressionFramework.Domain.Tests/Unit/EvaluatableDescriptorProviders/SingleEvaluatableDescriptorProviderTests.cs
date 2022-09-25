namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class SingleEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new SingleEvaluatableDescriptorProvider();

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(1);
    }
}
