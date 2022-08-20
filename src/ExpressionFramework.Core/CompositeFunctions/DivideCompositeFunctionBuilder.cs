namespace ExpressionFramework.Core.CompositeFunctions;

public class DivideCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new DivideCompositeFunction();
}
