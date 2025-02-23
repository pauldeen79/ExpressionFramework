namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringReplaceExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_FindExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringReplaceExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")), new TypedConstantExpression<string>("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_FindExpression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression(default(string)!)
            .WithReplaceExpression("f")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("FindExpression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ReplaceExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringReplaceExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantExpression<string>("f"), new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_ReplaceExpression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .WithReplaceExpression(default(string)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("ReplaceExpression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression(default(string)!)
            .WithFindExpression("e")
            .WithReplaceExpression("f")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Replaced_Value_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .WithReplaceExpression("f")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("Hello world".Replace("e", "f"));
    }

    [Fact]
    public void EvaluateTyped_Returns_Replaced_Value_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .WithReplaceExpression("f")
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("Hello world".Replace("e", "f"));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new StringReplaceExpressionBuilder()
            .WithExpression("A")
            .WithFindExpression("B")
            .WithReplaceExpression("C")
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<StringReplaceExpression>();
    }
}
