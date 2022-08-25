namespace ExpressionFramework.Core.Tests.TestHelpers;

public class ExpressionEvaluatorProviderMock : IExpressionEvaluatorProvider
{
    public Func<object?, object?, IExpression, IExpressionEvaluator, Tuple<bool, object?>> Delegate { get; set; }
        = new((_, _, _, _) => new Tuple<bool, object?>(default, default));

    public Result<object?> Evaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator)
    {
        var x = Delegate.Invoke(item, context, expression, evaluator);

        return x.Item1
            ? Result<object?>.Success(x.Item2)
            : Result<object?>.NotSupported();
    }
}
