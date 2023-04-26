namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class StringFindExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_FindExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringFindExpression(new TypedConstantExpression<string>("Hello world"), new TypedConstantResultExpression<string>(Result<string>.Error("Kaboom")));

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
        var sut = new StringFindExpression("Hello world", default!);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("FindExpression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Returns_Non_String_Value()
    {
        // Arrange
        var sut = new StringFindExpression(new DefaultExpression<string>(), new TypedConstantExpression<string>("e"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Position_Of_FindExpression_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringFindExpression("Hello world", "e");

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world".IndexOf("e"));
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new StringFindExpression("A", "B");

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<StringFindExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new StringFindExpressionBase(new TypedConstantExpression<string>(string.Empty), new TypedConstantExpression<string>(string.Empty));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }


    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new StringFindExpression("Hello world", "Hello");

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
    }

    [Fact]
    public void EvaluateTyped_Returns_Position_Of_FindExpression_When_Both_Expressions_Are_String()
    {
        // Arrange
        var sut = new StringFindExpression("Hello world", "e");

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world".IndexOf("e"));
    }
}
