namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class TypedTypedConstantExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public TypedTypedConstantExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns<FunctionParseResult, IExpressionParser, object?>((result, parser, context) => result.FunctionName switch
                      {
                          "MyDelegate" => Result<object?>.Success("some value"),
                          "MyString" => Result<object?>.Success("literal"),
                          _ => Result<object?>.Error("Kaboom")
                      });
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new TypedConstantExpressionParser();
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new TypedConstantExpressionParser();
        var functionParseResult = new FunctionParseResult("TypedConstant<System.String>", new[] { new FunctionArgument(new FunctionParseResult("MyDelegate", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("some value");
    }

    [Fact]
    public void Parse_Returns_Success_For_Wrong_FunctionResult()
    {
        // Arrange
        var parser = new TypedConstantExpressionParser();
        var functionParseResult = new FunctionParseResult("TypedConstant<System.Int32>", new[] { new FunctionArgument(new FunctionParseResult("MyString", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().BeEquivalentTo("Could not create typed constant expression. Error: Constructor on type 'ExpressionFramework.Domain.Expressions.TypedConstantExpression`1[[System.Int32, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]' not found.");
    }

    [Fact]
    public void Parse_Returns_Error_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new TypedConstantExpressionParser();
        var functionParseResult = new FunctionParseResult("TypedConstant<System.String>", new[] { new FunctionArgument(new FunctionParseResult("UNKNOWN_FUNCTION", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Parse_Returns_Success_For_Missing_GenericType()
    {
        // Arrange
        var parser = new TypedConstantExpressionParser();
        var functionParseResult = new FunctionParseResult("TypedConstant", new[] { new FunctionArgument(new FunctionParseResult("MyDelegate", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().BeEquivalentTo("No type defined");
    }
}
