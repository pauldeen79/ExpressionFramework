namespace ExpressionFramework.Parser.AggregatorResultParsers;

public abstract class AggregatorParserBase : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return Result.FromExistingResult<object?>(DoParse(context));
    }

    protected abstract Result<Aggregator> DoParse(FunctionCallContext context);
}
