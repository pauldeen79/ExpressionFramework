namespace ExpressionFramework.Parser.EvaluatableResultParsers;

public abstract class EvaluatableParserBase : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        if (!IsNameValid(context.FunctionCall.Name))
        {
            return Result.Continue<object?>();
        }

        return Result.FromExistingResult<object?>(DoParse(context));
    }

    public Result Validate(FunctionCallContext context)
        => Result.Success();

    protected virtual bool IsNameValid(string functionName)
    {
        functionName = ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        //TODO: Check whether we can do this without reflection (generate with constructor argument again?)
        return functionName.WithoutGenerics().Equals(GetType().GetCustomAttribute<FunctionNameAttribute>()?.Name, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual bool IsFunctionValid(FunctionCallContext context)
        => IsNameValid(ArgumentGuard.IsNotNull(context, nameof(context)).FunctionCall.Name);

    protected abstract Result<Evaluatable> DoParse(FunctionCallContext context);
}
