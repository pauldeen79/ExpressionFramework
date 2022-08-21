namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class ConditionalExpressionBuilder
{
    public ConditionalExpressionBuilder WithResultExpression(IExpressionBuilder resultExpression)
        => this.With(x => x.ResultExpression = resultExpression);

    public ConditionalExpressionBuilder WithFunction(IExpressionFunctionBuilder? function)
        => this.With(x => x.Function = function);
}
