namespace ExpressionFramework.Core.Operators
{
#nullable enable
    public partial record IsNotNullOrWhiteSpaceOperator
    {
        public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        {
            throw new NotImplementedException();
        }
    }
#nullable restore
}
