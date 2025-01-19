namespace ExpressionFramework.Parser;

public class ExpressionFrameworkParser : IExpressionFrameworkParser
{
    private readonly IEnumerable<IExpressionResolver> _resolvers;

    public ExpressionFrameworkParser(IEnumerable<IExpressionResolver> resolvers)
    {
        ArgumentGuard.IsNotNull(resolvers, nameof(resolvers));

        _resolvers = resolvers;
    }

    public Result<Expression> ParseExpression(FunctionCallContext context)
    {
        context = ArgumentGuard.IsNotNull(context, nameof(context));

        return _resolvers
            .Select(x => x.ParseExpression(context))
            .FirstOrDefault(x => x.Status != ResultStatus.Continue)
                ?? Result.Invalid<Expression>($"Unknown expression: {context.FunctionCall.Name}");
    }
}
