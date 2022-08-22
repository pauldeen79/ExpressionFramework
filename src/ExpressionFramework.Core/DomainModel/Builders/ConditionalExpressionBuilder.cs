namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class ConditionalExpressionBuilder
{
    public ConditionalExpressionBuilder WithResultExpression(IExpressionBuilder resultExpression)
        => this.With(x => x.ResultExpression = resultExpression);
}
