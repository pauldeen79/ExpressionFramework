namespace ExpressionFramework.Parser.Extensions;

public static class ExpressionFrameworkParserExtensions
{
    public static Result<ITypedExpression<T>> ParseExpression<T>(this IExpressionFrameworkParser instance, FunctionCallContext context)
    {
        var result = instance.ParseExpression(context);
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
