namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedFieldExpression<T> : IExpression, ITypedExpression<T>
{
    [Required]
    IExpression Expression { get; }
    [Required]
    ITypedExpression<string> FieldNameExpression { get; }
}
