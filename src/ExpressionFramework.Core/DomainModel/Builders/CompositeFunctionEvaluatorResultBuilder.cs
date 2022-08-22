namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class CompositeFunctionEvaluatorResultBuilder
{
    public static CompositeFunctionEvaluatorResultBuilder Supported
        => new CompositeFunctionEvaluatorResultBuilder().WithShouldContinue().WithIsSupported();

    public static CompositeFunctionEvaluatorResultBuilder NotSupported
        => new CompositeFunctionEvaluatorResultBuilder().WithShouldContinue();

    public static CompositeFunctionEvaluatorResultBuilder Error(string errorMessage)
        => new CompositeFunctionEvaluatorResultBuilder().WithIsSupported().WithErrorMessage(errorMessage);

    public CompositeFunctionEvaluatorResultBuilder WithShouldContinue(bool shouldContinue = true)
        => this.With(x => x.ShouldContinue = shouldContinue);

    public CompositeFunctionEvaluatorResultBuilder WithIsSupported(bool isSupported = true)
        => this.With(x => x.IsSupported = isSupported);

    public CompositeFunctionEvaluatorResultBuilder WithResult(object? result)
        => this.With(x => x.Result = result);

    public CompositeFunctionEvaluatorResultBuilder WithErrorMessage(string? errorMessage)
        => this.With(x => x.ErrorMessage = errorMessage);

    public CompositeFunctionEvaluatorResultBuilder Stop()
        => WithShouldContinue(false);
}
