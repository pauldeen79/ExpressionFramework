namespace ExpressionFramework.Core.Functions;

public class MultiplyCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public string Name { get; set; } = nameof(MultiplyCompositeFunction);

    public ICompositeFunction Build()
        => new MultiplyCompositeFunction();
}
