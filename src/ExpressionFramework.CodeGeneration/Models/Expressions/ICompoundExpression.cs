namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Executes an aggregator on two expressions")]
public interface ICompoundExpression : IExpression
{
    [Required][ValidateObject][Description("Expression to use as first expression in aggregator")] IExpression FirstExpression { get; }
    [Required][ValidateObject][Description("Expression to use as second expression in aggregator")] IExpression SecondExpression { get; }
    [Required][ValidateObject][Description("Aggregator to evaluate")] IAggregator Aggregator { get; }
    [ValidateObject] ITypedExpression<IFormatProvider>? FormatProviderExpression { get; }
}
