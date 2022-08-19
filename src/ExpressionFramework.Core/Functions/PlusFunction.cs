namespace ExpressionFramework.Core.Functions;

public record PlusFunction : IExpressionFunction
{
    public PlusFunction(IExpression plusExpression, IExpressionFunction? innerFunction)
    {
        InnerFunction = innerFunction;
        PlusExpression = plusExpression;
    }

    public IExpressionFunction? InnerFunction { get; }
    public IExpression PlusExpression { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new PlusFunctionBuilder()
            .WithPlusExpression(PlusExpression.ToBuilder())
            .WithInnerFunction(InnerFunction?.ToBuilder());
}
