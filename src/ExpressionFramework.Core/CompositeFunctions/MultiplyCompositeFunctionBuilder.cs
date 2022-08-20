namespace ExpressionFramework.Core.CompositeFunctions;

public class MultiplyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new MultiplyCompositeFunction();
}
