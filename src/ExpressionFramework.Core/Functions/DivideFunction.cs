namespace ExpressionFramework.Core.Functions;

public record DivideFunction : IExpressionFunction
{
    public DivideFunction(IExpression divideByExpression, IExpressionFunction? innerFunction)
    {
        InnerFunction = innerFunction;
        DivideByExpression = divideByExpression;
    }

    public IExpressionFunction? InnerFunction { get; }
    public IExpression DivideByExpression { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new DivideFunctionBuilder()
            .WithDivideByExpression(DivideByExpression.ToBuilder())
            .WithInnerFunction(InnerFunction?.ToBuilder());
}
