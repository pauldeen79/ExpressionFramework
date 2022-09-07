namespace CodeGenerationNext.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    Func<IDelegateExpressionRequest, object?> Value { get; }
}

