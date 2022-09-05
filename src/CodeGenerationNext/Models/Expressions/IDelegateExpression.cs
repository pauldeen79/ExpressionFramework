namespace CodeGenerationNext.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    Func<ExpressionEvaluatorRequest, object?> Value { get; }
}

