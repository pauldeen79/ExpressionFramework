namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class MinusCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new MinusCompositeFunction();
}
