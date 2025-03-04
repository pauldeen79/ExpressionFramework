namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Aggregates context with other expressions")]
public interface IAggregateExpression : IExpression
{
    [Required][ValidateObject][Description("Aggregator to evaluate")] IReadOnlyCollection <IExpression> Expressions { get; }
    [Required][ValidateObject][Description("Expressions to use in aggregator")] IAggregator Aggregator { get; }
    [ValidateObject][Description("Optional format provider to use on parsing numeric or date values")] ITypedExpression<IFormatProvider>? FormatProviderExpression { get; }
}
