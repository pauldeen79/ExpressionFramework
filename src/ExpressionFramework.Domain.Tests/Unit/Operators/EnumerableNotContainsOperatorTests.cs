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
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeTrue();
    }

    [Fact]
    public void Evaluate_Returns_False_When_LeftSequence_Contains_Right_Value()
    {
        // Arrange
        var sut = new EnumerableNotContainsOperator();

        // Act
        var actual = sut.Evaluate(null, new ConstantExpression(new[] { "A", "B", "C" }), new ConstantExpression("B"));

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LeftValue_Is_Not_An_Enumerable()
    {
        // Arrange
        var sut = new EnumerableNotContainsOperator();

        // Act
        var actual = sut.Evaluate(null, new ConstantExpression(1), new ConstantExpression("B"));

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(EnumerableNotContainsOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EnumerableNotContainsOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeTrue();
        result.RightValueTypeName.Should().NotBeEmpty();
        result.ReturnValues.Should().HaveCount(2);
    }
}
