namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class IsGreaterOrEqualOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(IsGreaterOrEqualOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(IsGreaterOrEqualOperator));
        result.Parameters.ShouldBeEmpty();
        result.UsesLeftValue.ShouldBeTrue();
        result.LeftValueTypeName.ShouldNotBeEmpty();
        result.UsesRightValue.ShouldBeTrue();
        result.RightValueTypeName.ShouldNotBeEmpty();
        result.ReturnValues.ShouldHaveSingleItem();
    }

    [Fact]
    public void Different_Types_Returns_Invalid()
    {
        // Arrange
        var sut = new IsGreaterOrEqualOperator();

        // Act
        var result = sut.Evaluate(null, new ConstantExpression("string value"), new ConstantExpression(1));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Object must be of type String.");
    }
}
