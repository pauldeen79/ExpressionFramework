namespace ExpressionFramework.Parser.OperatorResultParsers;

public abstract class OperatorParserBase : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return Result.FromExistingResult<object?>(DoParse(context));
    }

    protected abstract Result<Operator> DoParse(FunctionCallContext context);
}
