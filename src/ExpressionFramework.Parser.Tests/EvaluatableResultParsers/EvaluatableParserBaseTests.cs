namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public class EvaluatableParserBaseTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public EvaluatableParserBaseTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Success(new Mock<Evaluatable>().Object));
    }

    [Fact]
    public void Ctor_Throws_On_Null_FunctionName()
    {
        // Act & Assert
        this.Invoking(_ => new MyEvaluatableParser(functionName: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionName");
    }

    [Fact]
    public void Parse_Throws_On_Null_FunctionParseResult()
    {
        // Arrange
        var parser = new MyEvaluatableParser();

        // Act & Assert
        this.Invoking(_ => Parse(parser, functionParseResult: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionParseResult");
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new MyEvaluatableParser();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Wrong").Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Parse_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new MyEvaluatableParser();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Correct").Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<Evaluatable>();
    }

    private Result<object?> Parse(MyEvaluatableParser parser, FunctionParseResult functionParseResult)
        => parser.Parse
        (
            functionParseResult,
            null,
            _evaluatorMock.Object,
            _parserMock.Object
        );

    private sealed class MyEvaluatableParser : EvaluatableParserBase
    {
        public MyEvaluatableParser(string functionName) : base(functionName)
        {
        }

        public MyEvaluatableParser() : base("Correct") { }

        protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
            => Result<Evaluatable>.FromExistingResult(evaluator.Evaluate(functionParseResult, parser));
    }
}
