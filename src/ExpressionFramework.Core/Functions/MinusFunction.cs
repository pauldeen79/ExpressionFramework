namespace ExpressionFramework.Core.Functions;

public record MinusFunction : IExpressionFunction
{
    public MinusFunction(IExpression minusExpression, IExpressionFunction? innerFunction)
    {
        InnerFunction = innerFunction;
        MinusExpression = minusExpression;
    }

    public IExpressionFunction? InnerFunction { get; }
    public IExpression MinusExpression { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new MinusFunctionBuilder()
            .WithMinusExpression(MinusExpression.ToBuilder())
            .WithInnerFunction(InnerFunction?.ToBuilder());
}
