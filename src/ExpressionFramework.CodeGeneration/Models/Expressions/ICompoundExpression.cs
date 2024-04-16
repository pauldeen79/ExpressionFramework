namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICompoundExpression : IExpression
{
    [Required][ValidateObject] IExpression FirstExpression { get; }
    [Required][ValidateObject] IExpression SecondExpression { get; }
    [Required][ValidateObject] IAggregator Aggregator { get; }
    [ValidateObject] ITypedExpression<IFormatProvider>? FormatProviderExpression { get; }
}
