namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunction
{
    protected EvaluatableParserBase(string functionName)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));
    }

    public Result<object?> Evaluate(FunctionCallContext context)
        => Result.FromExistingResult<object?>(DoParse(context));

    public Result Validate(FunctionCallContext context)
        => Result.Success();

    protected abstract Result<Evaluatable> DoParse(FunctionCallContext context);
}
