namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates two expressions, and compares the two results. It will return true when they are not equal, or false otherwise.")]
public interface INotEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("First expression to perform Not Equals operation on")] IExpression FirstExpression { get; }
    [Required][ValidateObject][Description("Second expression to perform Not Equals operation on")] IExpression SecondExpression { get; }
}
