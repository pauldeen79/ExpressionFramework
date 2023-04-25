namespace ExpressionFramework.CodeGeneration.Models.Contracts;

// Note that this class is a work-around for the fact that ModelFramework.CodeGeneration does not handle IReadOnlyCollection<ITypedExpression<T>> or ITypedExpression<IReadOnlyCollection<T>> well...
// Which is probably a shortcoming in the overrides on the ExpressionFrameworkCodeGenerationBase, but it's a little hard to tackle this problem... So for now, we stick with this work-around
public interface IMultipleTypedExpressions<T> : ITypedExpression<IEnumerable<T>>
{
    IReadOnlyCollection<T> Expressions { get; }
}
