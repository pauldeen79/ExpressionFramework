﻿namespace ExpressionFramework.Core.CompositeFunctions;

public class DivideCompositeFunction : ICompositeFunction
{
    public ICompositeFunctionBuilder ToBuilder()
        => new DivideCompositeFunctionBuilder();
}
