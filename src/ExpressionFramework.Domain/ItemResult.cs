namespace ExpressionFramework.Domain;

public class ItemResult
{
    public ItemResult(object? item, Result<bool> result)
    {
        Item = item;
        Result = result.IsNotNull(nameof(result));
    }

    public object? Item { get; }
    public Result<bool> Result { get; }
}
