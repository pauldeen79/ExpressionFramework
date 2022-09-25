namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class ConstantEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ConstantEvaluatableDescriptorProvider();

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Parameters.Should().HaveCount(1);
        result.ReturnValues.Should().HaveCount(1);
    }
}
