namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface ITypedFieldExpression<T> : IExpression, ITypedExpression<T>
{
    [Required][ValidateObject] IExpression Expression { get; }
    [Required][ValidateObject] ITypedExpression<string> FieldNameExpression { get; }
}
