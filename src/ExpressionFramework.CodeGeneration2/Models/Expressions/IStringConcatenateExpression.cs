namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConcatenateExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] IMultipleTypedExpressions<string> Expressions { get; }
}
