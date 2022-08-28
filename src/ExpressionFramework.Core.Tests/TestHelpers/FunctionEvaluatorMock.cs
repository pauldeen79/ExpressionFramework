namespace ExpressionFramework.Core.Tests.TestHelpers;

public class FunctionEvaluatorMock : IFunctionEvaluator
{
    public Func<IExpressionFunction, object?, object?, IExpression, IExpressionEvaluator, Tuple<bool, object?>> Delegate { get; set; }
        = new((_, _, _, _, _) => new Tuple<bool, object?>(default, default));

    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        var x = Delegate.Invoke(function, value, sourceItem, expression, evaluator);

        result = x.Item2;
        return x.Item1;
    }
}
