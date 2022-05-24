namespace ExpressionFramework.Core.Functions;

public record ContainsFunction : IExpressionFunction
{
    public ContainsFunction(IExpression expression, object? objectToContain, IExpressionFunction? innerFunction)
    {
        Expression = expression;
        ObjectToContain = objectToContain;
        InnerFunction = innerFunction;
    }

    public IExpression Expression { get; }
    public object? ObjectToContain { get; }
    public IExpressionFunction? InnerFunction { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new ContainsFunctionBuilder().WithExpression(Expression.ToBuilder())
                                        .WithObjectToContain(ObjectToContain)
                                        .WithInnerFunction(InnerFunction?.ToBuilder());
}
