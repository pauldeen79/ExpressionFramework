namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Concatenates strings")]
public interface IStringConcatenateExpression : IExpression, ITypedExpression<string>
{
    [Required][ValidateObject][Description("Strings to concatenate")] IReadOnlyCollection <ITypedExpression<string>> Expressions { get; }
}
