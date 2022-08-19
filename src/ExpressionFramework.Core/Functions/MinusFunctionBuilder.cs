namespace ExpressionFramework.Core.Functions;

public class MinusFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionBuilder MinusExpression { get; set; } = new EmptyExpressionBuilder();
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public IExpressionFunction Build()
        => new MinusFunction(MinusExpression.Build(), InnerFunction?.Build());

    public MinusFunctionBuilder WithMinusExpression(IExpressionBuilder minusExpression)
        => this.With(x => x.MinusExpression = minusExpression);

    public MinusFunctionBuilder WithMinusExpression(object constantExpression)
        => this.With(x => x.MinusExpression = new ConstantExpressionBuilder(constantExpression));
}
