namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class DivideCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new DivideCompositeFunction();
}
