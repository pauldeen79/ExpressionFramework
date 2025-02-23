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
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(SingleEvaluatable));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.ShouldHaveSingleItem();
    }
}
