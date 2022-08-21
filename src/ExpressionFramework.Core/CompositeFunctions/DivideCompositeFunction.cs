namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class DivideCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new DivideCompositeFunctionBuilder();
}
