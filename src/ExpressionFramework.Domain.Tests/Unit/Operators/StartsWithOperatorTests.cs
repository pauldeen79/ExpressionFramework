namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class StartsWithOperatorTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_LeftValue_Is_Null()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new EmptyExpression(), new ConstantExpression("B"));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_RightValue_Is_Null()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new ConstantExpression("A"), new EmptyExpression());

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_True_When_RightValue_Is_StringEmpty()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new ConstantExpression("A"), new ConstantExpression(string.Empty));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeTrue();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(StartsWithOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(StartsWithOperator));
        result.Parameters.ShouldBeEmpty();
        result.UsesLeftValue.ShouldBeTrue();
        result.LeftValueTypeName.ShouldNotBeEmpty();
        result.UsesRightValue.ShouldBeTrue();
        result.RightValueTypeName.ShouldNotBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
    }
}
