namespace ExpressionFramework.CodeGeneration.Models.Expressions;

public interface IDelegateResultExpression : IExpression
{
    [CsharpTypeName("System.Func<System.Object?,CrossCutting.Common.Results.Result<System.Object?>>")]
    [Required] Func<object?, Result<object?>> Result { get; }
}
