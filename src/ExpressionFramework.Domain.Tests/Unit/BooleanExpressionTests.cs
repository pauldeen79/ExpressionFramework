namespace ExpressionFramework.Domain.Tests.Unit;

public class BooleanExpressionTests
{
    [Fact]
    public void GetDescriptor_Throws_On_Null_Type()
    {
        Action a = () => _ = BooleanExpression.GetDescriptor(type: null!, string.Empty, string.Empty, string.Empty, null, string.Empty);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Invalid_When_First_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, null!, new TypedConstantExpression<bool>(true), (x, y) => x && y);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("First expression is required");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Error_When_First_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantResultExpression<bool>(Result.Error<bool>("Kaboom")), new TypedConstantExpression<bool>(true), (x, y) => x && y);

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Invalid_When_Second_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), null!, (x, y) => x && y);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Second expression is required");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_Error_When_Second_Is_Error()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), new TypedConstantResultExpression<bool>(Result.Error<bool>("Kaboom")), (x, y) => x && y);

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateBooleanCombination_Returns_INvalid_When_Delegate_Is_Null()
    {
        // Act
        var result = BooleanExpression.EvaluateBooleanCombination(null, new TypedConstantExpression<bool>(true), new TypedConstantExpression<bool>(true), null!);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Delegate is required");
    }

}
