namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IFieldExpression : IExpression
{
    [Required][ValidateObject] IExpression Expression { get; }
    [Required][ValidateObject] ITypedExpression<string> FieldNameExpression { get; }
}
