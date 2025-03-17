namespace ExpressionFramework.Core.Operators;

public partial record StringNotContainsOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => leftValue is string leftString && rightValue is string rightString
            ? Result.Success(leftString.IndexOf(rightString, StringComparison.CurrentCultureIgnoreCase) == -1)
            : Result.Invalid<bool>("LeftValue and RightValue both need to be of type string");
}
