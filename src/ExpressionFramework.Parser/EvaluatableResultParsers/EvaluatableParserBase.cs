namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return Result.FromExistingResult<object?>(DoParse(context));
    }

    public Result Validate(FunctionCallContext context)
        => Result.Success();

    protected abstract Result<Evaluatable> DoParse(FunctionCallContext context);
}
