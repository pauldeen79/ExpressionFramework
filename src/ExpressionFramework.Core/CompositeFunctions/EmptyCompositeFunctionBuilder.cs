namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class EmptyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new EmptyCompositeFunction();
}
