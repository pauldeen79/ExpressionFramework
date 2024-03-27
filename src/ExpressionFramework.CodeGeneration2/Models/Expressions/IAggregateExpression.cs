namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IAggregateExpression : IExpression
{
    [Required][ValidateObject] IReadOnlyCollection<IExpression> Expressions { get; }
    [Required][ValidateObject] IAggregator Aggregator { get; }
    [ValidateObject] ITypedExpression<IFormatProvider>? FormatProviderExpression { get; }
}
