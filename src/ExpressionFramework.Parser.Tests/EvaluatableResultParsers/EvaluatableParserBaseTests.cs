namespace ExpressionFramework.Parser.Tests.EvaluatableResultParsers;

public class EvaluatableParserBaseTests
{
    private readonly IFunctionEvaluator _functionEvaluatorMock = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator _expressionEvaluatorMock = Substitute.For<IExpressionEvaluator>();

    public EvaluatableParserBaseTests()
    {
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<FunctionEvaluatorSettings>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(Substitute.For<Evaluatable>()));
    }

    [Fact]
    public void Evaluate_Throws_On_Null_Context()
    {
        // Arrange
        var parser = new MyEvaluatableParser();

        // Act & Assert
        Action a = () => _ = parser.Evaluate(context: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("context");
    }

    [Fact]
    public void Evaluate_Returns_Success_For_Correct_FunctionName()
    {
        // Arrange
        var parser = new MyEvaluatableParser();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("Correct").Build(), _functionEvaluatorMock, _expressionEvaluatorMock, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = parser.Evaluate(functionCallContext);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<Evaluatable>();
    }

    [FunctionName("Correct")]
    private sealed class MyEvaluatableParser : EvaluatableParserBase
    {
        protected override Result<Evaluatable> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Evaluatable>(context.FunctionEvaluator.Evaluate(context.FunctionCall, new FunctionEvaluatorSettingsBuilder()));
    }
}
