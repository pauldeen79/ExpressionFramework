namespace ExpressionFramework.Core.Operators;

public partial record NotEndsWithOperator
{
    public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        => leftValue is string leftString && rightValue is string rightString
            ? Result.Success(!leftString.EndsWith(rightString, stringComparison))
            : Result.Invalid<bool>("LeftValue and RightValue both need to be of type string");
}
