namespace ExpressionFramework.Core.Extensions;

public static class ResultExtensions
{
    public static bool IsSupported(this Result instance)
        => instance.Status != ResultStatus.NotSupported;

    public static bool ShouldContinue(this Result<IAggregateFunctionResultValue?> instance)
        => !instance.IsSupported() || (instance.Value?.Continue ?? false);

    public static object? GetResultValue(this Result<IAggregateFunctionResultValue?> instance)
        => instance.Value?.Value;
}
