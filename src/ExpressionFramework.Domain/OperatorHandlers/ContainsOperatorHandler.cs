namespace ExpressionFramework.Domain.OperatorHandlers;

public class ContainsOperatorHandler : OperatorHandlerBase<ContainsOperator>
{
    protected override bool Handle(object? leftValue, object? rightValue)
        => IsValid(leftValue, rightValue);

    internal static bool IsValid(object? leftValue, object? rightValue)
        => StringContains(leftValue, rightValue) || SequenceContainsItem(leftValue, rightValue);

    private static bool SequenceContainsItem(object? leftValue, object? rightValue)
        => leftValue is IEnumerable enumerable
            && !(rightValue is IEnumerable && rightValue is not string)
            && enumerable.OfType<object>().Contains(rightValue);

    private static bool StringContains(object? leftValue, object? rightValue)
        => leftValue is string leftString
            && rightValue is string rightString
            && !string.IsNullOrEmpty(rightString)
            && leftString.IndexOf(rightString, StringComparison.CurrentCultureIgnoreCase) > -1;
}

