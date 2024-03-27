namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ISequenceExpression : IExpression, ITypedExpression<IEnumerable<object?>>
{
    [Required][ValidateObject] IReadOnlyCollection<IExpression> Expressions { get; }
}
