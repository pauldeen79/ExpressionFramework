namespace ExpressionFramework.Core.Operators
{
#nullable enable
    public partial record NotEqualsOperator
    {
        public override CrossCutting.Common.Results.Result<bool> Evaluate(object? leftValue, object? rightValue, System.StringComparison stringComparison)
        {
            throw new System.NotImplementedException();
        }
    }
#nullable restore
}
