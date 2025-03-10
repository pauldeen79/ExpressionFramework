namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringFindExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_FindExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringFindExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")));

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
        var sut = new StringFindExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression(default(string)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("FindExpression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringFindExpression(new DefaultExpression<string>(), new TypedConstantExpression<string>("e"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Position_Of_FindExpression_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringFindExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("Hello world".IndexOf('e'));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new StringFindExpressionBuilder()
            .WithExpression("A")
            .WithFindExpression("B")
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<StringFindExpression>();
    }

    [Fact]
    public void EvaluateTyped_Returns_Position_Of_FindExpression_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringFindExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("Hello world".IndexOf('e'));
    }
}
