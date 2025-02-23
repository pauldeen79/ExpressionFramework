namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class StringContainsOperatorTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Left_Value_Is_Not_String()
    {
        // Act
        var result = new StringContainsOperator().Evaluate(null, new EmptyExpression(), new ConstantExpression("B"));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Right_Value_Is_Not_String()
    {
        // Act
        var result = new StringContainsOperator().Evaluate(null, new ConstantExpression("A"), new EmptyExpression());

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(StringContainsOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(StringContainsOperator));
        result.Parameters.ShouldBeEmpty();
        result.UsesLeftValue.ShouldBeTrue();
        result.LeftValueTypeName.ShouldNotBeEmpty();
        result.UsesRightValue.ShouldBeTrue();
        result.RightValueTypeName.ShouldNotBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
    }
}
