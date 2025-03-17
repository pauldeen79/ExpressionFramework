namespace ExpressionFramework.Core.Operators;

public partial record EnumerableNotContainsOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => leftValue is IEnumerable enumerableLeft
            ? Result.Success(!enumerableLeft.OfType<object>().Contains(rightValue))
            : Result.Invalid<bool>("Left value is not of type IEnumerable");
}
