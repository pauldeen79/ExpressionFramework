namespace ExpressionFramework.Parser.Tests.OperatorResultParsers;

public class OperatorParserBaseTests
{
    private readonly IFunctionEvaluator _functionEvaluatorMock = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator _expressionEvaluatorMock = Substitute.For<IExpressionEvaluator>();

    public OperatorParserBaseTests()
    {
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<FunctionEvaluatorSettings>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(Substitute.For<Operator>()));
    }

    [Fact]
    public void Evaluate_Throws_On_Null_Context()
    {
        // Arrange
        var parser = new MyOperatorParser();

        // Act & Assert
        Action a = () => _ = parser.Evaluate(context: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("context");
    }

    [Fact]
    public void Evaluate_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new MyOperatorParser();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Correct").Build(), _functionEvaluatorMock, _expressionEvaluatorMock, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = parser.Evaluate(functionCallContext);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<Operator>();
    }

    [FunctionName("Correct")]
    private sealed class MyOperatorParser : OperatorParserBase
    {
        protected override Result<Operator> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Operator>(context.FunctionEvaluator.Evaluate(context.FunctionCall, new FunctionEvaluatorSettingsBuilder()));
    }
}
