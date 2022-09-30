namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class SingleEvaluatableTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(SingleEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SingleEvaluatable));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().ContainSingle();
    }
}
