namespace ExpressionFramework.Core.CompositeFunctions;

public class EmptyCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new EmptyCompositeFunctionBuilder();
}
