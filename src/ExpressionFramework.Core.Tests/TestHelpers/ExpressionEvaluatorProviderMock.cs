namespace ExpressionFramework.Core.Tests.TestHelpers;

public class ExpressionEvaluatorProviderMock : IExpressionEvaluatorProvider
{
    public Func<object?, object?, IExpression, IExpressionEvaluator, Tuple<bool, object?>> Delegate { get; set; }
        = new((_, _, _, _) => new Tuple<bool, object?>(default, default));

    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        var x = Delegate.Invoke(item, context, expression, evaluator);

        result = x.Item2;
        return x.Item1;
    }
}
