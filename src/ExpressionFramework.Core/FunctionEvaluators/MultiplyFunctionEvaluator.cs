namespace ExpressionFramework.Core.FunctionEvaluators;

public class MultiplyFunctionEvaluator : IFunctionEvaluator
{
    public bool TryEvaluate(IExpressionFunction function, object? value, object? sourceItem, IExpressionEvaluator evaluator, out object? result)
    {
        result = null;
        if (!(function is MultiplyFunction multiplyFunction))
        {
            return false;
        }

        var divideByValue = evaluator.Evaluate(sourceItem, multiplyFunction.MultiplyByExpression);
        if (divideByValue is byte b)
        {
            result = Convert.ToByte(value) * b;
        }
        else if (divideByValue is short s)
        {
            result = Convert.ToInt16(value) * s;
        }
        else if (divideByValue is int i)
        {
            result = Convert.ToInt32(value) * i;
        }
        else if (divideByValue is long l)
        {
            result = Convert.ToInt64(value) * l;
        }
        else if (divideByValue is float flt)
        {
            result = Convert.ToSingle(value) * flt;
        }
        else if (divideByValue is double dbl)
        {
            result = Convert.ToDouble(value) * dbl;
        }
        else if (divideByValue is decimal dec)
        {
            result = Convert.ToDecimal(value) * dec;
        }

        return true;
    }
}
