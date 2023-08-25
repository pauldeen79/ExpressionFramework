namespace ExpressionFramework.Parser.Tests.AggregatorResultParsers;

public class AggregatorParserBaseTests
{
    private readonly Mock<IFunctionParseResultEvaluator> _evaluatorMock = new();
    private readonly Mock<IExpressionParser> _parserMock = new();

    public AggregatorParserBaseTests()
    {
        _evaluatorMock.Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
                      .Returns(Result<object?>.Success(new Mock<Aggregator>().Object));
    }

    [Fact]
    public void Ctor_Throws_On_Null_FunctionName()
    {
        // Act & Assert
        this.Invoking(_ => new MyAggregatorParser(functionName: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionName");
    }

    [Fact]
    public void Parse_Throws_On_Null_FunctionParseResult()
    {
        // Arrange
        var parser = new MyAggregatorParser();

        // Act & Assert
        this.Invoking(_ => Parse(parser, functionParseResult: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("functionParseResult");
    }

    [Fact]
    public void Parse_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new MyAggregatorParser();
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
        var parser = new MyAggregatorParser();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("Correct").Build();

        // Act
        var result = Parse(parser, functionParseResult);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<Aggregator>();
    }

    private Result<object?> Parse(MyAggregatorParser parser, FunctionParseResult functionParseResult)
        => parser.Parse
        (
            functionParseResult,
            null,
            _evaluatorMock.Object,
            _parserMock.Object
        );

    private sealed class MyAggregatorParser : AggregatorParserBase
    {
        public MyAggregatorParser(string functionName) : base(functionName!)
        {
        }

        public MyAggregatorParser() : base("Correct") { }

        protected override Result<Aggregator> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
            => Result<Aggregator>.FromExistingResult(evaluator.Evaluate(functionParseResult, parser));
    }
}
