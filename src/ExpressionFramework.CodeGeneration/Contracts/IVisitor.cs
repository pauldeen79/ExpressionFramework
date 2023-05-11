namespace ExpressionFramework.CodeGeneration.Contracts;

public interface IVisitor
{
    void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase;

    int Order { get; }
}
