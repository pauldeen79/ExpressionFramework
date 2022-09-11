namespace ExpressionFramework.CodeGeneration.Models.Requests
{
    public interface IDelegateExpressionRequest
    {
        object? Context { get; }
        [Required]
        IExpressionEvaluator Evaluator { get; }
    }
}
