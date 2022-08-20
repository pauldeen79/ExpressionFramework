namespace ExpressionFramework.Core.CompositeFunctions;

public class PlusCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new PlusCompositeFunction();
}
