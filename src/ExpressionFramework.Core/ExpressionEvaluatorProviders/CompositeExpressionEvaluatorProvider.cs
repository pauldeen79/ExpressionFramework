namespace ExpressionFramework.Core.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProvider : IExpressionEvaluatorProvider
{
    public bool TryEvaluate(object? item, object? context, IExpression expression, IExpressionEvaluator evaluator, out object? result)
    {
        result = default;

        if (expression is ICompositeExpression compositeExpression)
        {
            bool first = true;
            foreach (var innerExpression in compositeExpression.Expressions)
            {
                if (first)
                {
                    result = evaluator.Evaluate(context, item, innerExpression);
                    first = false;
                }
                else
                {
                    result = compositeExpression.CompositeFunction.Combine(result, item, evaluator, innerExpression);
                }
            }

            return true;
        }

        return false;
    }
}
