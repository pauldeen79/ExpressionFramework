namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Returns the value of a field (property) of the specified expression")]
public interface IFieldExpression : IExpression
{
    [Required][ValidateObject][Description("Expression to get the field (property) value for")] IExpression Expression { get; }
    [Required][ValidateObject][Description("Name of the field (property). Can also be nested, like Address.Street")] ITypedExpression<string> FieldNameExpression { get; }
}
