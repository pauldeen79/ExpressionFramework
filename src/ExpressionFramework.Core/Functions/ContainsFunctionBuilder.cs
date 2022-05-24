namespace ExpressionFramework.Core.Functions;

public class ContainsFunctionBuilder : IExpressionFunctionBuilder
{
    public IExpressionBuilder Expression { get; set; } = new EmptyExpressionBuilder();
    public object? ObjectToContain { get; set; }
    public IExpressionFunctionBuilder? InnerFunction { get; set; }

    public ContainsFunctionBuilder WithExpression(IExpressionBuilder expression)
        => this.Chain(x => x.Expression = expression);

    public ContainsFunctionBuilder WithObjectToContain(object? objectToContain)
        => this.Chain(x => x.ObjectToContain = objectToContain);

    public IExpressionFunction Build()
        => new ContainsFunction(Expression.Build(), ObjectToContain, InnerFunction?.Build());
}
