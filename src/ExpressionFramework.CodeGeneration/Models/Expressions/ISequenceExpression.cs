namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns all supplied expressions into a sequence (enumerable)")]
public interface ISequenceExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject][Description("Expressions to put in a sequence (enumerable)")] IReadOnlyCollection<IExpression> Expressions { get; }
}
