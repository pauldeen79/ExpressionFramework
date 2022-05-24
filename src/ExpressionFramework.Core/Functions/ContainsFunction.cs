namespace ExpressionFramework.Core.Functions;

public record ContainsFunction : IExpressionFunction
{
    public ContainsFunction(object? objectToContain, IExpressionFunction? innerFunction)
    {
        ObjectToContain = objectToContain;
        InnerFunction = innerFunction;
    }

    public object? ObjectToContain { get; }
    public IExpressionFunction? InnerFunction { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new ContainsFunctionBuilder().WithObjectToContain(ObjectToContain)
                                        .WithInnerFunction(InnerFunction?.ToBuilder());
}
