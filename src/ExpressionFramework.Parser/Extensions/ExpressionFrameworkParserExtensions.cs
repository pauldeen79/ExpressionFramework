namespace ExpressionFramework.Parser.Extensions;

public static class ExpressionFrameworkParserExtensions
{
    public static Result<ITypedExpression<T>> Parse<T>(this IExpressionFrameworkParser instance, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = instance.Parse(functionParseResult, evaluator, parser);
        if (!result.IsSuccessful())
        {
            return Result<ITypedExpression<T>>.FromExistingResult(result);
        }

        if (result.Value is ITypedExpression<T> t)
        {
            return Result<ITypedExpression<T>>.Success(t);
        }

        return Result<ITypedExpression<T>>.Invalid($"Expression is not a typed expression of type {typeof(T).FullName}");
    }
}
