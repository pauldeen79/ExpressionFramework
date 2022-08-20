namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class MultiplyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new MultiplyCompositeFunction();
}
