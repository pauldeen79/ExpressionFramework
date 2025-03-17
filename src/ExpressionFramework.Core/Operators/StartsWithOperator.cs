namespace ExpressionFramework.Core.Operators;

public partial record StartsWithOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => leftValue is string leftString && rightValue is string rightString
            ? Result.Success(leftString.StartsWith(rightString, StringComparison.CurrentCultureIgnoreCase))
            : Result.Invalid<bool>("LeftValue and RightValue both need to be of type string");
}
