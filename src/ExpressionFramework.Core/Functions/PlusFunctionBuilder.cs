namespace ExpressionFramework.Core.Functions;

public class PlusFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionBuilder PlusExpression { get; set; } = new EmptyExpressionBuilder();
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public IExpressionFunction Build()
        => new PlusFunction(PlusExpression.Build(), InnerFunction?.Build());

    public PlusFunctionBuilder WithPlusExpression(IExpressionBuilder plusExpression)
        => this.With(x => x.PlusExpression = plusExpression);

    public PlusFunctionBuilder WithPlusExpression(object constantExpression)
        => this.With(x => x.PlusExpression = new ConstantExpressionBuilder(constantExpression));
}
