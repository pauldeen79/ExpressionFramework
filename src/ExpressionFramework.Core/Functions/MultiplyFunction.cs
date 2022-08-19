namespace ExpressionFramework.Core.Functions;

public record MultiplyFunction : IExpressionFunction
{
    public MultiplyFunction(IExpression multiplyByExpression, IExpressionFunction? innerFunction)
    {
        InnerFunction = innerFunction;
        MultiplyByExpression = multiplyByExpression;
    }

    public IExpressionFunction? InnerFunction { get; }
    public IExpression MultiplyByExpression { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new MultiplyFunctionBuilder()
            .WithMultiplyByExpression(MultiplyByExpression.ToBuilder())
            .WithInnerFunction(InnerFunction?.ToBuilder());
}
