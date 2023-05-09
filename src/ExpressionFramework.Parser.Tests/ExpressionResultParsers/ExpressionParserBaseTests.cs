namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class ExpressionParserBaseTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();
    private readonly Mock<Expression> _expressionMock = new();

    public ExpressionParserBaseTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Success(_expressionMock.Object));

        _parserMock.Setup(x => x.Parse(It.IsAny<string>(), It.IsAny<IFormatProvider>(), It.IsAny<object?>()))
                   .Returns<string, IFormatProvider, object?>((value, formatProvider, context)
                    => int.TryParse(value, formatProvider, out var result)
                        ? Result<object?>.Success(result)
                        : Result<object?>.Success(value));

        _expressionMock.Setup(x => x.Evaluate(It.IsAny<object?>()))
                       .Returns(Result<object?>.Success("evaluated value"));
    }

    [Fact]
    public void Parse_Without_Context_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Wrong")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Without_Context_Returns_Success_With_Expression_As_Value_For_Correct_FunctionName()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<Expression>();
    }

    [Fact]
    public void Parse_With_Context_Returns_Success_With_Evaluated_Value_For_Correct_FunctionName()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("evaluated value");
    }

    [Fact]
    public void Parse_With_Context_Returns_Success_With_Null_Value_For_Correct_FunctionName_When_Value_Was_Empty()
    {
        // Arrange
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Success(null));
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Parse_With_Context_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Wrong")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_With_Context_Returns_Failure_When_Parse_Fails()
    {
        // Arrange
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Error("Kaboom"));
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Generic_Type_Is_Missing()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();
        
        // Act
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("No type defined");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Argument_Parsing_Fails()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct<System.String>")
            .Build();

        // Act
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Missing argument: name");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Argument_Is_Of_Wrong_Type()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct<System.String>")
            .AddArguments(new LiteralArgumentBuilder().WithValue("1"))
            .Build();

        // Act
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could not create TypedConstantExpression. Error: Constructor on type 'ExpressionFramework.Domain.Expressions.TypedConstantExpression`1[[System.String, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]' not found.");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Success_When_Generic_Type_Is_Present_And_Argument_Is_Of_Correct_Type()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct<System.String>")
            .AddArguments(new LiteralArgumentBuilder().WithValue("string value"))
            .Build();

        // Act
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock.Object, _parserMock.Object);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    private sealed class MyExpressionParser : ExpressionParserBase
    {
        public MyExpressionParser() : base("Correct") { }

        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
            => Result<Expression>.FromExistingResult(evaluator.Evaluate(functionParseResult, parser));

        public Result<Expression> DoParseTypedExpression(Type expressionType, int index, string argumentName, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
          => ParseTypedExpression(expressionType, index, argumentName, functionParseResult, evaluator, parser);
    }
}
