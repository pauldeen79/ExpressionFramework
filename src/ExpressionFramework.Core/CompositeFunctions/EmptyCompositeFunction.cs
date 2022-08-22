namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
internal class EmptyCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new EmptyCompositeFunctionBuilder();
}
