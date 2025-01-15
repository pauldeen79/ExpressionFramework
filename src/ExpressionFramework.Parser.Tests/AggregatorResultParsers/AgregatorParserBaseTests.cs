namespace ExpressionFramework.Parser.Tests.AggregatorResultParsers;

public class AggregatorParserBaseTests
{
    private readonly IFunctionEvaluator _functionEvaluatorMock = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator _expressionEvaluatorMock = Substitute.For<IExpressionEvaluator>();

    public AggregatorParserBaseTests()
    {
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<IExpressionEvaluator>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(Substitute.For<Aggregator>()));
    }

    [Fact]
    public void Evaluate_Throws_On_Null_Context()
    {
        // Arrange
        var parser = new MyAggregatorParser();

        // Act & Assert
        this.Invoking(_ => parser.Evaluate(context: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("context");
    }

    [Fact]
    public void Evaluate_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var parser = new MyAggregatorParser();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Wrong").Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Evaluate(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void Evaluate_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new MyAggregatorParser();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Correct").Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = parser.Evaluate(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<Aggregator>();
    }

    [FunctionName("Correct")]
    private sealed class MyAggregatorParser : AggregatorParserBase
    {
        protected override Result<Aggregator> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Aggregator>(context.FunctionEvaluator.Evaluate(context.FunctionCall, context.ExpressionEvaluator));
    }
}
