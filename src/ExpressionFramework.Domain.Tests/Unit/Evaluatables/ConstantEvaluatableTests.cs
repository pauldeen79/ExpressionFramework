namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class ConstantEvaluatableTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Evaluate_Return_Input_Value(bool input)
    {
        // Arrange
        var sut = new ConstantEvaluatable(input);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be(input);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var evaluatable = new ConstantEvaluatableBase(false);

        // Act & Assert
        evaluatable.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ConstantEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ConstantEvaluatable));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
    }
}
