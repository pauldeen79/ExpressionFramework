namespace CodeGenerationNext.Models.Expressions;

public interface IDelegateExpression : IExpression
{
    Func<IExpressionEvaluatorRequest, object?> Value { get; }
}

