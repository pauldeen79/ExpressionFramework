﻿namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public class DelegateExpressionParserTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public DelegateExpressionParserTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<object?>()))
                      .Returns<FunctionParseResult, object?>((result, context) => result.FunctionName switch
                        {
                            "MyDelegate" => Result<object?>.Success(new Func<object?, object?>(_ => "some value")),
                            "MyString" => Result<object?>.Success("literal"),
                            _ => Result<object?>.Error("Kaboom")
                        });
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new DelegateExpressionParser(_parserMock.Object);
        var contextValue = "the context value";
        var functionParseResult = new FunctionParseResult("Wrong", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, contextValue);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new DelegateExpressionParser(_parserMock.Object);
        var functionParseResult = new FunctionParseResult("Delegate", new[] { new FunctionArgument(new FunctionParseResult("MyDelegate", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("some value");
    }

    [Fact]
    public void Parse_Returns_Success_For_Wrong_FunctionResult()
    {
        // Arrange
        var parser = new DelegateExpressionParser(_parserMock.Object);
        var functionParseResult = new FunctionParseResult("Delegate", new[] { new FunctionArgument(new FunctionParseResult("MyString", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().BeEquivalentTo("Value is not of type delegate (Func<object?, object?>)");
    }

    [Fact]
    public void Parse_Returns_Error_When_ArgumentParsing_Fails()
    {
        // Arrange
        var parser = new DelegateExpressionParser(_parserMock.Object);
        var functionParseResult = new FunctionParseResult("Delegate", new[] { new FunctionArgument(new FunctionParseResult("UNKNOWN_FUNCTION", Enumerable.Empty<FunctionParseResultArgument>(), CultureInfo.InvariantCulture, null)) }, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Parse(functionParseResult, null, _evaluatorMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}