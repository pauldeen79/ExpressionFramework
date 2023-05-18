namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedExpressionModelVisitor : IVisitor
{
    public int Order => 32;

    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase => BuilderVisitorBase.Visit
        (
            typeBaseBuilder,
            context,
            Constants.Namespaces.DomainModelsExpressions,
            "ToTypedEntity",
            "ToEntity",
            "Model"
        );
}
