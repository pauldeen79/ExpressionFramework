namespace ExpressionFramework.Core.CompositeFunctions;

public class MinusCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new MinusCompositeFunctionBuilder();
}
