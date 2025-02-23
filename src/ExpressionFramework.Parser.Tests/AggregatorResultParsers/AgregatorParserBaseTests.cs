namespace ExpressionFramework.Parser.Tests.AggregatorResultParsers;

public class AggregatorParserBaseTests
{
    private readonly IFunctionEvaluator _functionEvaluatorMock = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator _expressionEvaluatorMock = Substitute.For<IExpressionEvaluator>();

    public AggregatorParserBaseTests()
    {
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<FunctionEvaluatorSettings>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(Substitute.For<Aggregator>()));
    }

    [Fact]
    public void Evaluate_Throws_On_Null_Context()
    {
        // Arrange
        var parser = new MyAggregatorParser();

        // Act & Assert
        Action a = () => _ = parser.Evaluate(context: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("context");
    }

    [Fact]
    public void Evaluate_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new MyAggregatorParser();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Correct").Build(), _functionEvaluatorMock, _expressionEvaluatorMock, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = parser.Evaluate(functionCallContext);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<Aggregator>();
    }

    [FunctionName("Correct")]
    private sealed class MyAggregatorParser : AggregatorParserBase
    {
        protected override Result<Aggregator> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Aggregator>(context.FunctionEvaluator.Evaluate(context.FunctionCall, new FunctionEvaluatorSettingsBuilder()));
    }
}
