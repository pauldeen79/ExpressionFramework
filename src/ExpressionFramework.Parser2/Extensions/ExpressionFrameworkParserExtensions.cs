namespace ExpressionFramework.Parser.Extensions;

public static class ExpressionFrameworkParserExtensions
{
    public static Result<ITypedExpression<T>> Parse<T>(this IExpressionFrameworkParser instance, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = instance.Parse(functionParseResult, evaluator, parser);
        if (!result.IsSuccessful())
        {
            return Result.FromExistingResult<ITypedExpression<T>>(result);
        }

        if (result.Value is ITypedExpression<T> t)
        {
            return Result.Success(t);
        }

        return Result.Invalid<ITypedExpression<T>>($"Expression is not a typed expression of type {typeof(T).FullName}");
    }
}
