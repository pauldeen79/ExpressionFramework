namespace ExpressionFramework.Core.Default;

public class ConditionEvaluator : IConditionEvaluator
{
    private readonly IExpressionEvaluator _evaluator;

    public ConditionEvaluator(IExpressionEvaluator evaluator)
        => _evaluator = evaluator;

    public Result<bool> Evaluate(object? context, IEnumerable<ICondition> conditions)
    {
        var builder = new StringBuilder();
        foreach (var condition in conditions)
        {
            if (builder.Length > 0)
            {
                builder.Append(condition.Combination == Combination.And ? "&" : "|");
            }

            var prefix = condition.StartGroup ? "(" : string.Empty;
            var suffix = condition.EndGroup ? ")" : string.Empty;
            var itemResult = IsItemValid(context, condition);
            if (!itemResult.IsSuccessful())
            {
                return itemResult;
            }
            builder.Append(prefix)
                   .Append(itemResult.Value ? "T" : "F")
                   .Append(suffix);
        }

        return EvaluateBooleanExpression(builder.ToString());
    }

    private Result<bool> IsItemValid(object? item, ICondition condition)
    {
        var leftResult = _evaluator.Evaluate(item, item, condition.LeftExpression);
        if (!leftResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(leftResult);
        }

        var rightResult = _evaluator.Evaluate(item, item, condition.RightExpression);
        if (!rightResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(rightResult);
        }

        if (Operators.Items.TryGetValue(condition.Operator, out var predicate))
        {
            return Result<bool>.Success(predicate.Invoke(new OperatorData(leftResult.Value, rightResult.Value)));
        }

        return Result<bool>.Invalid($"Unsupported operator: {condition.Operator}");
    }

    private static Result<bool> EvaluateBooleanExpression(string expression)
    {
        var result = ProcessRecursive(ref expression);
        if (!result.IsSuccessful())
        {
            return result;
        }

        var @operator = "&";
        foreach (var character in expression)
        {
            bool currentResult;
            switch (character)
            {
                case '&':
                    @operator = "&";
                    break;
                case '|':
                    @operator = "|";
                    break;
                case 'T':
                case 'F':
                    currentResult = character == 'T';
                    result = @operator == "&"
                        ? Result<bool>.Success(result.Value && currentResult)
                        : Result<bool>.Success(result.Value || currentResult);
                    break;
            }
        }

        return result;
    }

    private static Result<bool> ProcessRecursive(ref string expression)
    {
        var result = Result<bool>.Success(true);
        var openIndex = -1;
        int closeIndex;
        do
        {
            closeIndex = expression.IndexOf(")");
            if (closeIndex > -1)
            {
                openIndex = expression.LastIndexOf("(", closeIndex);
                if (openIndex > -1)
                {
                    result = EvaluateBooleanExpression(expression.Substring(openIndex + 1, closeIndex - openIndex - 1));
                    expression = string.Concat(GetPrefix(expression, openIndex),
                                               GetCurrent(result),
                                               GetSuffix(expression, closeIndex));
                }
            }
        } while (closeIndex > -1 && openIndex > -1);
        return result;
    }

    private static string GetPrefix(string expression, int openIndex)
        => openIndex == 0
            ? string.Empty
            : expression.Substring(0, openIndex - 2);

    private static string GetCurrent(Result<bool> result)
        => result.Value
            ? "T"
            : "F";

    private static string GetSuffix(string expression, int closeIndex)
        => closeIndex == expression.Length
            ? string.Empty
            : expression.Substring(closeIndex + 1);
}
