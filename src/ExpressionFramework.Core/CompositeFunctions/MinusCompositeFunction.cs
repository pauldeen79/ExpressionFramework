namespace ExpressionFramework.Core.CompositeFunctions;

[ExcludeFromCodeCoverage]
public class MinusCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new MinusCompositeFunctionBuilder();
}
