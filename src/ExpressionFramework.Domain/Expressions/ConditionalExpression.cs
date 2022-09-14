﻿namespace ExpressionFramework.Domain.Expressions;

public partial record ConditionalExpression
{
    public ConditionalExpression(IEnumerable<Condition> conditions) : this(conditions, new ConstantExpression(true), new ConstantExpression(false))
    {
    }

    public override Result<object?> Evaluate(object? context)
    {
        var evaluationResult = EvaluateConditions(context, Conditions);

        if (!evaluationResult.IsSuccessful())
        {
            return Result<object?>.FromExistingResult(evaluationResult);
        }

        if (evaluationResult.Value)
        {
            return ResultExpression.Evaluate(context);
        }

        if (DefaultExpression != null)
        {
            return DefaultExpression.Evaluate(context);
        }

        return Result<object?>.Success(null);
    }

    public Result<bool> EvaluateAsBoolean(object? context)
    {
        var result = Evaluate(context);
        if (!result.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(result);
        }
        if (result.Value is not bool b)
        {
            return Result<bool>.Error($"The result could not be determined, because it is not of type boolean. Actual type: {result.Value?.GetType()?.FullName ?? "NULL"}");
        }
        return Result<bool>.Success(b);
    }

    private Result<bool> EvaluateConditions(object? context, IEnumerable<Condition> conditions)
    {
        if (CanEvaluateSimpleConditions(conditions))
        {
            return EvaluateSimpleConditions(context, conditions);
        }

        return EvaluateComplexConditions(context, conditions);
    }

    private bool CanEvaluateSimpleConditions(IEnumerable<Condition> conditions)
        => !conditions.Any(x => x.Combination == Combination.Or || x.StartGroup || x.EndGroup);

    private Result<bool> EvaluateSimpleConditions(object? context, IEnumerable<Condition> conditions)
    {
        foreach (var condition in conditions)
        {
            var itemResult = IsItemValid(context, condition);
            if (!itemResult.IsSuccessful())
            {
                return itemResult;
            }

            if (!itemResult.Value)
            {
                return itemResult;
            }
        }

        return Result<bool>.Success(true);
    }

    private Result<bool> EvaluateComplexConditions(object? context, IEnumerable<Condition> conditions)
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

        return Result<bool>.Success(EvaluateBooleanExpression(builder.ToString()));
    }

    private Result<bool> IsItemValid(object? context, Condition condition)
    {
        var leftResult = condition.LeftExpression.Evaluate(context);
        if (!leftResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(leftResult);
        }

        var rightResult = condition.RightExpression.Evaluate(context);
        if (!rightResult.IsSuccessful())
        {
            return Result<bool>.FromExistingResult(rightResult);
        }

        return condition.Operator.Evaluate(leftResult.Value, rightResult.Value);
    }

    private static bool EvaluateBooleanExpression(string expression)
    {
        var result = ProcessRecursive(ref expression);

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
                        ? result && currentResult
                        : result || currentResult;
                    break;
            }
        }

        return result;
    }

    private static bool ProcessRecursive(ref string expression)
    {
        var result = true;
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

    private static string GetCurrent(bool result)
        => result
            ? "T"
            : "F";

    private static string GetSuffix(string expression, int closeIndex)
        => closeIndex == expression.Length
            ? string.Empty
            : expression.Substring(closeIndex + 1);
}
