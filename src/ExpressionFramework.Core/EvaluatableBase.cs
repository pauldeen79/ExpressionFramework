namespace ExpressionFramework.Core;

public partial record EvaluatableBase
{
    public abstract Result<bool> Evaluate(object? context);
}
