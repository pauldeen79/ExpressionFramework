namespace ExpressionFramework.Core.CompositeFunctions;

public class MinusCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new MinusCompositeFunction();
}
