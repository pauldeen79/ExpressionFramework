﻿namespace ExpressionFramework.Core.CompositeFunctionEvaluators;

public record MultiplyCompositeFunctionEvaluator : ICompositeFunctionEvaluator
{
    public bool TryEvaluate(ICompositeFunction function,
                            bool isFirstItem,
                            object? previousValue,
                            object? context,
                            IExpressionEvaluator evaluator,
                            IExpression expression,
                            out object? result,
                            out bool shouldContinue)
    {
        shouldContinue = true;

        if (function is not MultiplyCompositeFunction)
        {
            result = null;
            return false;
        }

        if (isFirstItem)
        {
            result = previousValue;
            return true;
        }

        var currentValue = evaluator.Evaluate(context, context, expression);
        if (currentValue is byte b)
        {
            result = Convert.ToByte(previousValue) * b;
            return true;
        }
        else if (currentValue is short s)
        {
            result =  Convert.ToInt16(previousValue) * s;
            return true;
        }
        else if (currentValue is int i)
        {
            result = Convert.ToInt32(previousValue) * i;
            return true;
        }
        else if (currentValue is long l)
        {
            result = Convert.ToInt64(previousValue) * l;
            return true;
        }
        else if (currentValue is float flt)
        {
            result = Convert.ToSingle(previousValue) * flt;
            return true;
        }
        else if (currentValue is double dbl)
        {
            result = Convert.ToDouble(previousValue) * dbl;
            return true;
        }
        else if (currentValue is decimal dec)
        {
            result = Convert.ToDecimal(previousValue) * dec;
            return true;
        }

        result = default;
        return true;
    }
}
