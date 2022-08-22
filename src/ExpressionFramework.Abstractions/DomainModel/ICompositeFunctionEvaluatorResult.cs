namespace ExpressionFramework.Abstractions.DomainModel;

public interface ICompositeFunctionEvaluatorResult
{
    bool IsSupported { get; }
    object? Result { get; }
    string? ErrorMessage { get; }
    bool ShouldContinue { get; }
}
