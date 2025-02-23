namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class EnumerableNotContainsOperatorTests
{
    [Fact]
    public void Evaluate_Returns_True_When_LeftSequence_Does_Not_Contain_Right_Value()
    {
        // Arrange
        var sut = new EnumerableNotContainsOperator();

        // Act
        var actual = sut.Evaluate(null, new ConstantExpression(new[] { "A", "B", "C" }), new ConstantExpression("D"));

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBeTrue();
    }

    [Fact]
    public void Evaluate_Returns_False_When_LeftSequence_Contains_Right_Value()
    {
        // Arrange
        var sut = new EnumerableNotContainsOperator();

        // Act
        var actual = sut.Evaluate(null, new ConstantExpression(new[] { "A", "B", "C" }), new ConstantExpression("B"));

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBeFalse();
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LeftValue_Is_Not_An_Enumerable()
    {
        // Arrange
        var sut = new EnumerableNotContainsOperator();

        // Act
        var actual = sut.Evaluate(null, new ConstantExpression(1), new ConstantExpression("B"));

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(EnumerableNotContainsOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(EnumerableNotContainsOperator));
        result.Parameters.ShouldBeEmpty();
        result.UsesLeftValue.ShouldBeTrue();
        result.LeftValueTypeName.ShouldNotBeEmpty();
        result.UsesRightValue.ShouldBeTrue();
        result.RightValueTypeName.ShouldNotBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
    }
}
