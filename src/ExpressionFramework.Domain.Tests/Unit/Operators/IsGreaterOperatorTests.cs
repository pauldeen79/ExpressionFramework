namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class IsGreaterOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(IsGreaterOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(IsGreaterOperator));
        result.Parameters.Should().BeEmpty();
        result.UsesLeftValue.Should().BeTrue();
        result.LeftValueTypeName.Should().NotBeEmpty();
        result.UsesRightValue.Should().BeTrue();
        result.RightValueTypeName.Should().NotBeEmpty();
        result.ReturnValues.Should().ContainSingle();
    }

    [Fact]
    public void Different_Types_Returns_Invalid()
    {
        // Arrange
        var sut = new IsGreaterOperator();

        // Act
        var result = sut.Evaluate(null, new ConstantExpression("string value"), new ConstantExpression(1));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Object must be of type String.");
    }
}
