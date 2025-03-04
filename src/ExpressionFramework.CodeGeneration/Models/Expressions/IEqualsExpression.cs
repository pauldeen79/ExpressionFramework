namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates two expressions, and compares the two results. It will return true when they are equal, or false otherwise.")]
public interface IEqualsExpression : IExpression, ITypedExpression<bool>
{
    [Required][ValidateObject][Description("First expression to perform Equals operation on")] IExpression FirstExpression { get; }
    [Required][ValidateObject][Description("Second expression to perform Equals operation on")] IExpression SecondExpression { get; }
}
