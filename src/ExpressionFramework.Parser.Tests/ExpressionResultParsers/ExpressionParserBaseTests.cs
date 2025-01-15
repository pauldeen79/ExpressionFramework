namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class ExpressionParserBaseTests
{
    private readonly IFunctionParseResultEvaluator _evaluatorMock = Substitute.For<IFunctionParseResultEvaluator>();
    private readonly IExpressionParser _parserMock = Substitute.For<IExpressionParser>();
    private readonly Expression _expressionMock = Substitute.For<Expression>();

    public ExpressionParserBaseTests()
    {
        _evaluatorMock.Evaluate(Arg.Any<FunctionParseResult>(), Arg.Any<IExpressionParser>(), Arg.Any<object?>())
                      .Returns(Result.Success<object?>(_expressionMock));
        _parserMock.Parse(Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<object?>())
                   .Returns(x =>
                        int.TryParse(x.ArgAt<string>(0), x.ArgAt<IFormatProvider>(1), out var result)
                        ? Result.Success<object?>(result)
                        : Result.Success<object?>(x.ArgAt<string>(0)));

        _expressionMock.Evaluate(Arg.Any<object?>())
                       .Returns(Result.Success<object?>("evaluated value"));
    }

    [Fact]
    public void Ctor_Throws_On_Null_FunctionName()
    {
        // Act & Assert
        this.Invoking(_ => new MyExpressionParser(functionName: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionName");
    }

    [Fact]
    public void Parse_Without_Context_Throws_On_Null_FunctionParseResult()
    {
        // Act & Assert
        this.Invoking(_ => new MyExpressionParser().Parse(functionParseResult: null!, _evaluatorMock, _parserMock))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionParseResult");
    }

    [Fact]
    public void Parse_Without_Context_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Wrong")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, _evaluatorMock, _parserMock);

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
        var result = new MyExpressionParser().Parse(functionParseResult, _evaluatorMock, _parserMock);

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
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock, _parserMock);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("evaluated value");
    }

    [Fact]
    public void Parse_With_Context_Returns_Success_With_Null_Value_For_Correct_FunctionName_When_Value_Was_Empty()
    {
        // Arrange
        _evaluatorMock.Evaluate(Arg.Any<FunctionParseResult>(), Arg.Any<IExpressionParser>(), Arg.Any<object?>())
                      .Returns(Result.Success<object?>(null));
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock, _parserMock);

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
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock, _parserMock);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_With_Context_Returns_Failure_When_Parse_Fails()
    {
        // Arrange
        _evaluatorMock.Evaluate(Arg.Any<FunctionParseResult>(), Arg.Any<IExpressionParser>(), Arg.Any<object?>())
                      .Returns(Result.Error<object?>("Kaboom"));
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().Parse(functionParseResult, null, _evaluatorMock, _parserMock);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ParseTypedExpression_Throws_On_Null_ExpressionType()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();
        var sut = new MyExpressionParser();

        // Act & Assert
        sut.Invoking(x => x.DoParseTypedExpression(expressionType: null!, 0, string.Empty, functionParseResult, _evaluatorMock, _parserMock))
           .Should().Throw<ArgumentNullException>().WithParameterName("expressionType");
    }

    [Fact]
    public void ParseTypedExpression_Throws_On_Null_FunctionParseResult()
    {
        // Arrange
        var sut = new MyExpressionParser();

        // Act & Assert
        sut.Invoking(x => x.DoParseTypedExpression(typeof(string), 0, string.Empty, functionParseResult: null!, _evaluatorMock, _parserMock))
           .Should().Throw<ArgumentNullException>().WithParameterName("functionParseResult");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Generic_Type_Is_Missing()
    {
        // Arrange
        var functionParseResult = new FunctionParseResultBuilder()
            .WithFunctionName("Correct")
            .Build();

        // Act
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock, _parserMock);

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
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock, _parserMock);

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
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock, _parserMock);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could not create TypedConstantExpression. Error: Constructor on type 'ExpressionFramework.Domain.Expressions.TypedConstantExpression`1[[System.String, System.Private.CoreLib, Version=9.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]' not found.");
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
        var result = new MyExpressionParser().DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", functionParseResult, _evaluatorMock, _parserMock);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [Fact]
    public void IsNameValid_Throws_On_Null_FunctionName()
    {
        // Act
        this.Invoking(_ => new MyExpressionParser().IsNameValidPublic(functionName: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionName");
    }

    private sealed class MyExpressionParser : ExpressionParserBase
    {
        public MyExpressionParser(string functionName) : base(functionName)
        {
        }

        public MyExpressionParser() : base("Correct") { }

        protected override Result<Expression> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Expression>(evaluator.Evaluate(functionParseResult, parser));

        public Result<Expression> DoParseTypedExpression(Type expressionType, int index, string argumentName, FunctionCallContext context)
            => ParseTypedExpression(expressionType, index, argumentName, functionParseResult, evaluator, parser);

        public bool IsNameValidPublic(string functionName) => IsNameValid(functionName);
    }
}
