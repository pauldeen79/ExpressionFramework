namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class FirstCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public ICompositeFunction Build()
        => new FirstCompositeFunction();
}
