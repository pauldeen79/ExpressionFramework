namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class MultiplyCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new MultiplyCompositeFunctionBuilder();
}
