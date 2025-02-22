namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class IsSmallerOrEqualOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(IsSmallerOrEqualOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(IsSmallerOrEqualOperator));
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
        var sut = new IsSmallerOrEqualOperator();

        // Act
        var result = sut.Evaluate(null, new ConstantExpression("string value"), new ConstantExpression(1));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Object must be of type String.");
    }
}
