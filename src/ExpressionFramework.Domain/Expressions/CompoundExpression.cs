namespace ExpressionFramework.Domain.Expressions;

public partial record CompoundExpression
{
    public override Result<object?> Evaluate(object? context)
        => Aggregator.Aggregate(context, SecondExpression);

    public CompoundExpression(object? secondValue, Aggregator aggregator)
        : this(new ConstantExpression(secondValue), aggregator) { }
}

