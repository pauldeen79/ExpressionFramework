namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IStringConcatenateExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject] IReadOnlyCollection<ITypedExpression<string>> Expressions { get; }
}
