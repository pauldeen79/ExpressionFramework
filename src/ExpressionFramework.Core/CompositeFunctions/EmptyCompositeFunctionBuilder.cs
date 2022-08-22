namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
internal class EmptyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new EmptyCompositeFunction();
}
