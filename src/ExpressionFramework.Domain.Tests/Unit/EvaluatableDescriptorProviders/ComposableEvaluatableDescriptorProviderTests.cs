namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class ComposableEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ComposableEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Parameters.Should().HaveCount(6);
        result.ReturnValues.Should().HaveCount(1);
    }
}
