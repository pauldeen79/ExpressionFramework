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
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe(input);
    }

    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ConstantEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ConstantEvaluatable));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
    }
}
