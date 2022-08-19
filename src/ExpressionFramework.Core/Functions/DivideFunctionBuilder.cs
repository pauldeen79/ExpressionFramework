namespace ExpressionFramework.Core.Functions;

public class DivideFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionBuilder DivideByExpression { get; set; } = new EmptyExpressionBuilder();
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public IExpressionFunction Build()
        => new DivideFunction(DivideByExpression.Build(), InnerFunction?.Build());

    public DivideFunctionBuilder WithDivideByExpression(IExpressionBuilder divideByExpression)
        => this.With(x => x.DivideByExpression = divideByExpression);

    public DivideFunctionBuilder WithDivideByExpression(object constantExpression)
        => this.With(x => x.DivideByExpression = new ConstantExpressionBuilder(constantExpression));
}
