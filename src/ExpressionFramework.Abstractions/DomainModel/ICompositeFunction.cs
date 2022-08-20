namespace ExpressionFramework.Abstractions.DomainModel;

//TODO: Introduce ICompositeFunctionEvaluator to separate the data and evaluation
public interface ICompositeFunction
{
    string Name { get; } //TODO: Remove this property, but first remove code generation and build empty composite function by hand.

    object? Combine(object? previousValue, object? context, IExpressionEvaluator evaluator, IExpression expression);
    ICompositeFunctionBuilder ToBuilder();
}
