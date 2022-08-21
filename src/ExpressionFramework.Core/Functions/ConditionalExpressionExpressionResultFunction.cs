namespace ExpressionFramework.Core.Functions;

//TODO: Check if we can remove this class
public record ConditionalExpressionExpressionResultFunction : IExpressionFunction
{
    public ConditionalExpressionExpressionResultFunction(IExpressionFunction? innerFunction)
        => InnerFunction = innerFunction;

    public IExpressionFunction? InnerFunction { get; }

    public IExpressionFunctionBuilder ToBuilder()
        => new ConditionalExpressionExpressionResultFunctionBuilder().WithInnerFunction(InnerFunction?.ToBuilder());
}
