namespace ExpressionFramework.Core.CompositeFunctions;

public class EmptyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new EmptyCompositeFunction();
}
