namespace ExpressionFramework.Core.Functions;

//TODO: Check if we can remove this class
public class ConditionalExpressionExpressionResultFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public IExpressionFunction Build()
        => new ConditionalExpressionExpressionResultFunction(InnerFunction?.Build());
}
