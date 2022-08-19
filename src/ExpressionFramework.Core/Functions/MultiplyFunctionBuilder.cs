namespace ExpressionFramework.Core.Functions;

public class MultiplyFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionBuilder MultiplyByExpression { get; set; } = new EmptyExpressionBuilder();
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public IExpressionFunction Build()
        => new MultiplyFunction(MultiplyByExpression.Build(), InnerFunction?.Build());

    public MultiplyFunctionBuilder WithMultiplyByExpression(IExpressionBuilder multiplyByExpression)
        => this.With(x => x.MultiplyByExpression = multiplyByExpression);

    public MultiplyFunctionBuilder WithMultiplyByExpression(object constantExpression)
        => this.With(x => x.MultiplyByExpression = new ConstantExpressionBuilder(constantExpression));
}
