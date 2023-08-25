namespace ExpressionFramework.Domain.Tests.Unit;

public class BooleanExpressionTests
{
    [Fact]
    public void GetDescriptor_Throws_On_Null_Type()
    {
        this.Invoking(_ => BooleanExpression.GetDescriptor(type: null!, string.Empty, string.Empty, string.Empty, null, string.Empty))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Invalid_When_First_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, null!, new TypedConstantExpression<bool>(true), (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("First expression is required");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Error_When_First_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantResultExpression<bool>(Result<bool>.Error("Kaboom")), new TypedConstantExpression<bool>(true), (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Invalid_When_Second_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), null!, (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Second expression is required");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Error_When_Second_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), new TypedConstantResultExpression<bool>(Result<bool>.Error("Kaboom")), (x, y) => x && y);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_INvalid_When_Delegate_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), new TypedConstantExpression<bool>(true), null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Delegate is required");
    }

}
