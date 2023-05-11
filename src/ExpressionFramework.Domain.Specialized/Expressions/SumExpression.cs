﻿namespace ExpressionFramework.Domain.Expressions;

[DynamicDescriptor(typeof(SumExpression))]
public partial record SumExpression
{
    public override Result<object?> Evaluate(object? context)
        => EnumerableExpression.GetAggregateValue(context, Expression, Sum, SelectorExpression);

    public static ExpressionDescriptor GetExpressionDescriptor()
        => EnumerableExpression.GetDescriptor
        (
            typeof(SumExpression),
            "Gets the sum from the (enumerable) expression, optionally using a selector expression",
            "Sum (could be decimal, double, float, long or int)",
            "This will be returned in case no error occurs",
            "Expression cannot be empty, Expression must be of type IEnumerable, Could only compute sum of numeric values",
            "This status (or any other status not equal to Ok) will be returned in case the selector evaluation returns something else than Ok",
            hasDefaultExpression: false,
            resultValueType: typeof(object)
        );

    private static Result<object?> Sum(IEnumerable<object?> value)
    {
        if (value.All(x => x is decimal))
        {
            return Result<object?>.Success(value.Select(Convert.ToDecimal).Sum());
        }

        if (value.All(x => x is double))
        {
            return Result<object?>.Success(value.Select(Convert.ToDouble).Sum());
        }

        if (value.All(x => x is float))
        {
            return Result<object?>.Success(value.Select(Convert.ToSingle).Sum());
        }

        if (value.All(x => x is long))
        {
            return Result<object?>.Success(value.Select(Convert.ToInt64).Sum());
        }

        if (value.All(x => x is int || x is short || x is byte))
        {
            return Result<object?>.Success(value.Select(Convert.ToInt32).Sum());
        }

        return Result<object?>.Invalid("Could only compute sum of numeric values");
    }
}
