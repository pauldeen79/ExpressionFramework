namespace ExpressionFramework.Core.Functions;

public class PlusCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public string Name { get; set; } = nameof(PlusCompositeFunction);

    public ICompositeFunction Build()
        => new PlusCompositeFunction();
}
