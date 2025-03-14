namespace ExpressionFramework.Core.Operators
{
#nullable enable
    public partial record IsSmallerOperator
    {
        public override CrossCutting.Common.Results.Result<bool> Evaluate(object? leftValue, object? rightValue, System.StringComparison stringComparison)
        {
            throw new System.NotImplementedException();
        }
    }
#nullable restore
}
