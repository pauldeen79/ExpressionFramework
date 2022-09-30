namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class StringContainsOperatorTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Left_Value_Is_Not_String()
    {
        // Act
        var result = new StringContainsOperator().Evaluate(null, new EmptyExpression(), new ConstantExpression("B"));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Right_Value_Is_Not_String()
    {
        // Act
        var result = new StringContainsOperator().Evaluate(null, new ConstantExpression("A"), new EmptyExpression());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(StringContainsOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(StringContainsOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeTrue();
        result.RightValueTypeName.Should().NotBeEmpty();
        result.ReturnValues.Should().HaveCount(2);
    }
}
