namespace ExpressionFramework.Domain.Evaluatables;

[EvaluatableDescription("Evaluates multiple conditions")]
[ParameterDescription(nameof(Conditions), "Conditions to evaluate")]
[ParameterRequired(nameof(Conditions), true)]
[ReturnValue(ResultStatus.Ok, typeof(bool), "true when the conditions evaluate to true, otherwise false", "This result will be returned when evaluation of the expressions succeed")]
public partial record ComposedEvaluatable : IValidatableObject
{
    public override Result<bool> Evaluate(object? context)
    {
        if (CanEvaluateSimpleConditions(Conditions))
        {
            return EvaluateSimpleConditions(context, Conditions);
        }

        return EvaluateComplexConditions(context, Conditions);
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var groupCounter = 0;
        var index = 0;
        foreach (var evaluatable in Conditions)
        {
            if (evaluatable.StartGroup)
            {
                groupCounter++;
            }
            if (evaluatable.EndGroup)
            {
                groupCounter--;
            }
            if (groupCounter < 0)
            {
                yield return new ValidationResult($"EndGroup not valid at index {index}, because there is no corresponding StartGroup", [nameof(Conditions)]);
                break;
            }

            index++;
        }

        if (groupCounter == 1)
        {
            yield return new ValidationResult("Missing EndGroup", [nameof(Conditions)]);
        }
#pragma warning disable S2583 // false positive!
        else if (groupCounter > 1)
#pragma warning restore S2583 // false positive!
        {
            yield return new ValidationResult($"{groupCounter} missing EndGroups", [nameof(Conditions)]);
        }
    }

    private static bool CanEvaluateSimpleConditions(IEnumerable<ComposableEvaluatable> conditions)
        => !conditions.Any(x =>
            (x.Combination ?? Combination.And) == Combination.Or
            || x.StartGroup
            || x.EndGroup
        );

    private static Result<bool> EvaluateSimpleConditions(object? context, IEnumerable<ComposableEvaluatable> conditions)
    {
        foreach (var evaluatable in conditions)
        {
            var itemResult = IsItemValid(context, evaluatable);
            if (!itemResult.IsSuccessful())
            {
                return itemResult;
            }

            if (!itemResult.Value)
            {
                return itemResult;
            }
        }

        return Result.Success(true);
    }

    private static Result<bool> EvaluateComplexConditions(object? context, IEnumerable<ComposableEvaluatable> conditions)
    {
        var builder = new StringBuilder();
        foreach (var evaluatable in conditions)
        {
            if (builder.Length > 0)
            {
                builder.Append(evaluatable.Combination == Combination.And ? "&" : "|");
            }

            var prefix = evaluatable.StartGroup ? "(" : string.Empty;
            var suffix = evaluatable.EndGroup ? ")" : string.Empty;
            var itemResult = IsItemValid(context, evaluatable);
            if (!itemResult.IsSuccessful())
            {
                return itemResult;
            }
            builder.Append(prefix)
                   .Append(itemResult.Value ? "T" : "F")
                   .Append(suffix);
        }

        return Result.Success(EvaluateBooleanExpression(builder.ToString()));
    }

    private static Result<bool> IsItemValid(object? context, ComposableEvaluatable condition)
        => condition.Evaluate(context);

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
