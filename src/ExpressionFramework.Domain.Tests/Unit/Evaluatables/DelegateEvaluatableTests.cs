namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class DelegateEvaluatableTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Evaluate_Return_Input_Value(bool input)
    {
        // Arrange
        var sut = new DelegateEvaluatable(() => input);

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be(input);
    }

    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(DelegateEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(DelegateEvaluatable));
        result.Parameters.Should().HaveCount(1);
        result.ReturnValues.Should().HaveCount(1);
    }
}
