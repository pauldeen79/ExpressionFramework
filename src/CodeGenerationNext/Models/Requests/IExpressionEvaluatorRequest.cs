namespace CodeGenerationNext.Models.Requests
{
    public interface IExpressionEvaluatorRequest
    {
        object? Context { get; }
        [Required]
        IExpressionEvaluator Evaluator { get; }
    }
}
