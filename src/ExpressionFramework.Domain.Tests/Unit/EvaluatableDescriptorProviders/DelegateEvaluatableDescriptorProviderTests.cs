namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class DelegateEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(DelegateEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Parameters.Should().HaveCount(1);
        result.ReturnValues.Should().HaveCount(1);
    }
}
