namespace ExpressionFramework.Core.Functions;

[FunctionArgument("Expressions", typeof(IEnumerable), "Expressions to aggregate")]
[FunctionArgument("Aggregator", typeof(Abstractions.IAggregator), "Aggregator to evaluate")]
[FunctionArgument("FormatProvider", typeof(IFormatProvider), "Optional format provider (default is invariant)", false)]
public class AggregateFunction : IFunction
{
    public Result<object?> Evaluate(FunctionCallContext context)
        => new ResultDictionaryBuilder()
            .Add("Expressions", () => context.GetArgumentValueResult<IEnumerable>(0, "Expressions"))
            .Add("Aggregator", () => context.GetArgumentValueResult<Abstractions.IAggregator>(1, "Aggregator"))
            .Add("FormatProvider", () => context.GetArgumentValueResult<IFormatProvider>(2, "FormatProvider", CultureInfo.InvariantCulture))
            .Build()
            .OnSuccess(results =>
            {
                var expressions = results.GetValue<IEnumerable>("Expressions").OfType<object>().ToArray();
                if (expressions.Length == 0)
                {
                    return Result.Invalid<object?>("Sequence contains no elements");
                }

                var result = Result.Success<object?>(expressions[0]);
                var aggregator = results.GetValue<Abstractions.IAggregator>("Aggregator");
                var formatProvider = results.GetValue<IFormatProvider>("FormatProvider");

                foreach (var expression in expressions.Skip(1))
                {
                    result = aggregator.Aggregate(result.Value!, expression, formatProvider);
                    if (!result.IsSuccessful())
                    {
                        return result;
                    }
                }

                return result;
            });
}
