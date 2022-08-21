namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class EmptyCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new EmptyCompositeFunctionBuilder();
}
