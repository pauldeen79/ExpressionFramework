namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public class EvaluatableParserBaseTests
{
    private readonly IFunctionParseResultEvaluator _evaluatorMock = Substitute.For<IFunctionParseResultEvaluator>();
    private readonly IExpressionParser _parserMock = Substitute.For<IExpressionParser>();

    public EvaluatableParserBaseTests()
    {
        _evaluatorMock.Evaluate(Arg.Any<FunctionParseResult>(), Arg.Any<IExpressionParser>(), Arg.Any<object?>())
                      .Returns(Result.Success<object?>(Substitute.For<Evaluatable>()));
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
            _evaluatorMock,
            _parserMock
        );

    private sealed class MyEvaluatableParser : EvaluatableParserBase
    {
        public MyEvaluatableParser(string functionName) : base(functionName)
        {
        }

        public MyEvaluatableParser() : base("Correct") { }

        protected override Result<Evaluatable> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
            => Result.FromExistingResult<Evaluatable>(evaluator.Evaluate(functionParseResult, parser));
    }
}
