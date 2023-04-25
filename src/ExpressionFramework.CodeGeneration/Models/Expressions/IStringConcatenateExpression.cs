namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConcatenateExpression : IExpression, ITypedExpression<string>
{
    [Required]
    IMultipleTypedExpressions<string> Expressions { get; }
}
