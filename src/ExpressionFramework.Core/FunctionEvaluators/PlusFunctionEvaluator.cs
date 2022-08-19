namespace ExpressionFramework.Core.FunctionEvaluators;

public class PlusFunctionEvaluator : IFunctionEvaluator
{
    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpressionEvaluator evaluator, out object? result)
    {
        result = null;
        if (!(function is PlusFunction plusFunction))
        {
            return false;
        }

        var plusValue = evaluator.Evaluate(sourceItem, plusFunction.PlusExpression);
        if (plusValue is byte b)
        {
            result = Convert.ToByte(value) + b;
        }
        else if (plusValue is short s)
        {
            result = Convert.ToInt16(value) + s;
        }
        else if (plusValue is int i)
        {
            result = Convert.ToInt32(value) + i;
        }
        else if (plusValue is long l)
        {
            result = Convert.ToInt64(value) + l;
        }
        else if (plusValue is float flt)
        {
            result = Convert.ToSingle(value) + flt;
        }
        else if (plusValue is double dbl)
        {
            result = Convert.ToDouble(value) + dbl;
        }
        else if (plusValue is decimal dec)
        {
            result = Convert.ToDecimal(value) + dec;
        }

        return true;
    }
}
