namespace ExpressionFramework.Core.Functions;

public class DivideCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public string Name { get; set; } = nameof(DivideCompositeFunction);

    public ICompositeFunction Build()
        => new DivideCompositeFunction();
}
