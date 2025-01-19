namespace ExpressionFramework.Parser.AggregatorResultParsers;

public abstract class AggregatorParserBase : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return Result.FromExistingResult<object?>(DoParse(context));
    }

    public Result Validate(FunctionCallContext context)
        => Result.Success();

    protected abstract Result<Aggregator> DoParse(FunctionCallContext context);
}
