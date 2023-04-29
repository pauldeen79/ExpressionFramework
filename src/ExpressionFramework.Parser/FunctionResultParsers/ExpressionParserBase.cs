using CrossCutting.Utilities.Parsers;
using ExpressionFramework.Domain.Expressions;

namespace ExpressionFramework.Parser.FunctionResultParsers;

public abstract class ExpressionParserBase : IFunctionResultParser, IExpressionResolver
{
    private readonly string _functionName;
    protected IExpressionParser Parser { get; }

    protected ExpressionParserBase(IExpressionParser parser, string functionName)
    {
        Parser = parser;
        _functionName = functionName;
    }

    public Result<object?> Parse(FunctionParseResult functionParseResult, object? context, IFunctionParseResultEvaluator evaluator)
    {
        var result = Parse(functionParseResult, evaluator);
        return result.IsSuccessful() && result.Status != ResultStatus.Continue
            ? result.Value!.Evaluate(context)
            : Result<object?>.FromExistingResult(result);
    }

    public Result<Expression> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        => functionParseResult.FunctionName.ToUpperInvariant() == _functionName.ToUpperInvariant()
            ? DoParse(functionParseResult, evaluator)
            : Result<Expression>.Continue();

    protected abstract Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator);

    protected TypedDelegateResultExpression<T> GetArgumentValue<T>(FunctionParseResult functionParseResult, int index, string argumentName, IFunctionParseResultEvaluator evaluator)
    {
        var delegateValueResult = functionParseResult.GetArgumentValue(index, argumentName, functionParseResult.Context, evaluator);
        if (!delegateValueResult.IsSuccessful())
        {
            return new TypedDelegateResultExpression<T>(_ => Result<T>.FromExistingResult(delegateValueResult));
        }

        if (delegateValueResult.Value is not T t)
        {
            return new TypedDelegateResultExpression<T>(_ => Result<T>.Invalid($"{argumentName} is not of type{typeof(T).FullName}"));
        }

        return new TypedDelegateResultExpression<T>(_ => Result<T>.Success(t));
    }
}
