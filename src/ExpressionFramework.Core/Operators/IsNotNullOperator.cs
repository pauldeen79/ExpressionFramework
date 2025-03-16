namespace ExpressionFramework.Core.Operators
{
#nullable enable
    public partial record IsNotNullOperator
    {
        public override Result<bool> Evaluate(object? leftValue, object? rightValue, StringComparison stringComparison)
        {
            throw new NotImplementedException();
        }
    }
#nullable restore
}
