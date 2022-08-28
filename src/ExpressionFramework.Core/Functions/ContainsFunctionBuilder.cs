namespace ExpressionFramework.Core.Functions;

public class ContainsFunctionBuilder : IExpressionFunctionBuilder
{
    public object? ObjectToContain { get; set; }
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public ContainsFunctionBuilder WithObjectToContain(object? objectToContain)
        => this.With(x => x.ObjectToContain = objectToContain);

    public IExpressionFunction Build()
        => new ContainsFunction(ObjectToContain, InnerFunction?.Build());
}
