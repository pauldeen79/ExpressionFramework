namespace ExpressionFramework.CodeGeneration.Models.Expressions;

//Example how to customize the expression name, with a namespace:
//[ExpressionName("Find", "String")]
//Example how to customize the expression name, without a namespace:
//[ExpressionName("StringFind")]
public interface IStringFindExpression : IExpression, ITypedExpression<int>
{
    [Required][ValidateObject] ITypedExpression<string> Expression { get; }
    [Required][ValidateObject] ITypedExpression<string> FindExpression { get; }
}
