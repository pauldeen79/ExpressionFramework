namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ICompoundExpression : IExpression
{
    [Required]
    IExpression FirstExpression { get; }
    [Required]
    IExpression SecondExpression { get; }
    [Required]
    IAggregator Aggregator { get; }
    ITypedExpression<IFormatProvider>? FormatProviderExpression { get; }
}
