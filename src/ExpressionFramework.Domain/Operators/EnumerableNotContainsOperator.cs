namespace ExpressionFramework.Domain.Operators;

[OperatorDescription("Determines whether the left value does not contain the right value")]
[OperatorUsesLeftValue(true)]
[OperatorLeftValueType(typeof(IEnumerable))]
[OperatorUsesRightValue(true)]
[OperatorRightValueType(typeof(object))]
[ReturnValue(ResultStatus.Ok, "true or false", "True when left value does not contain the right value, otherwise false")]
[ReturnValue(ResultStatus.Invalid, "Empty", "Left value is not of type IEnumerable")]
public partial record EnumerableNotContainsOperator
{
    protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
        => leftValue is IEnumerable enumerableLeft
            ? Result<bool>.Success(!enumerableLeft.OfType<object>().Contains(rightValue))
            : Result<bool>.Invalid("Left value is not of type IEnumerable");
}

