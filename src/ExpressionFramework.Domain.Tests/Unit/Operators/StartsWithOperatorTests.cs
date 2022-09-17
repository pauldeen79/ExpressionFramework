namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class StartsWithOperatorTests
{
    [Fact]
    public void Evaluate_Returns_False_When_LeftValue_Is_Null()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new EmptyExpression(), new ConstantExpression("B"));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_Returns_False_When_RightValue_Is_Null()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new ConstantExpression("A"), new EmptyExpression());

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_Returns_False_When_RightValue_Is_StringEmpty()
    {
        // Act
        var result = new StartsWithOperator().Evaluate(null, new ConstantExpression("A"), new ConstantExpression(string.Empty));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeFalse();
    }
}
