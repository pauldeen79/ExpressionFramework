namespace ExpressionFramework.Core.CompositeFunctions;

public class MultiplyCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new MultiplyCompositeFunctionBuilder();
}
