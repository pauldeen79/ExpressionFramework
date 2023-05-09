namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringReplaceExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_FindExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringReplaceExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantResultExpression<string>(Result<string>.Error("Kaboom")), new TypedConstantExpression<string>("f"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
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
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("FindExpression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ReplaceExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringReplaceExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantExpression<string>("f"), new TypedConstantResultExpression<string>(Result<string>.Error("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
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
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("ReplaceExpression is not of type string");
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
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type string");
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world".Replace("e", "f"));
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world".Replace("e", "f"));
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
        actual.Should().BeOfType<StringReplaceExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new StringReplaceExpressionBase(new TypedConstantExpression<string>(string.Empty), new TypedConstantExpression<string>(string.Empty), new TypedConstantExpression<string>(string.Empty));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new StringReplaceExpressionBuilder()
            .WithExpression("Hello world")
            .WithFindExpression("e")
            .WithReplaceExpression("f")
            .Build();

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }
}
