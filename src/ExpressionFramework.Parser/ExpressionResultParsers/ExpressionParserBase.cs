namespace ExpressionFramework.Parser.ExpressionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;
    private readonly string _namespace;
    private readonly string[] _aliases;

    protected ExpressionParserBase(string functionName, params string[] aliases) : this(functionName, string.Empty, aliases)
    {
    }

    protected ExpressionParserBase(string functionName, string @namespace, params string[] aliases)
    {
        ArgumentGuard.IsNotNull(functionName, nameof(functionName));
        ArgumentGuard.IsNotNull(@namespace, nameof(@namespace));
        ArgumentGuard.IsNotNull(aliases, nameof(aliases));

        _functionName = functionName;
        _namespace = @namespace;
        _aliases = aliases;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        var result = Parse(functionParseResult, evaluator, parser);

        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value?.Evaluate(context) ?? Result.Success<object?>(null)
            : Result.FromExistingResult<object?>(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        functionParseResult = ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult));

        return IsFunctionValid(functionParseResult)
            ? DoParse(functionParseResult, evaluator, parser)
            : Result.Continue<Expression>();
    }

    protected virtual bool IsNameValid(string functionName)
    {
        functionName = ArgumentGuard.IsNotNull(functionName, nameof(functionName));

        var lastDot = functionName.LastIndexOf('.');
        if (lastDot == -1)
        {
            // no namespace qualifier
            return _namespace.Length == 0 && (functionName.Equals(_functionName, StringComparison.OrdinalIgnoreCase) || Array.Exists(_aliases, x => functionName.Equals(x, StringComparison.OrdinalIgnoreCase)));
        }

        // namespace qualifier
        return functionName.Substring(0, lastDot).Equals(_namespace, StringComparison.OrdinalIgnoreCase)
            && functionName.Substring(lastDot + 1).Equals(_functionName, StringComparison.OrdinalIgnoreCase);
    }

    protected virtual bool IsFunctionValid(FunctionParseResult functionParseResult)
        => IsNameValid(ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult)).FunctionName);

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser);

    protected static Result<Expression> ParseTypedExpression(Type expressionType, int index, string argumentName, FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
    {
        expressionType = ArgumentGuard.IsNotNull(expressionType, nameof(expressionType));
        functionParseResult = ArgumentGuard.IsNotNull(functionParseResult, nameof(functionParseResult));

        var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();
        if (!typeResult.IsSuccessful())
        {
            return Result.FromExistingResult<Expression>(typeResult);
        }

        var valueResult = functionParseResult.GetArgumentValueResult(index, argumentName, functionParseResult.Context, evaluator, parser);
        if (!valueResult.IsSuccessful())
        {
            return Result.FromExistingResult<Expression>(valueResult);
        }

#pragma warning disable CA1031 // Do not catch general exception types
        try
        {
            return Result.Success((Expression)Activator.CreateInstance(expressionType.MakeGenericType(typeResult.Value!), valueResult.Value));
        }
        catch (Exception ex)
        {
            return Result.Invalid<Expression>($"Could not create {expressionType.Name.Replace("`1", string.Empty)}. Error: {ex.Message}");
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }
}
