namespace ExpressionFramework.Core.Functions;

public class MinusCompositeFunctionBuilder : ICompositeFunctionBuilder
{
    public string Name { get; set; } = nameof(MinusCompositeFunction);

    public ICompositeFunction Build()
        => new MinusCompositeFunction();
}
