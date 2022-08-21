namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class PlusCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new PlusCompositeFunctionBuilder();
}
