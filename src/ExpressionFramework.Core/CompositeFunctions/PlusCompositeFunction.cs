namespace ExpressionFramework.Core.CompositeFunctions;

public class PlusCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new PlusCompositeFunctionBuilder();
}
