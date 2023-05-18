namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderVisitor : IVisitor
{
    public int Order => 31;

    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase => BuilderVisitorBase.Visit
        (
            typeBaseBuilder,
            context,
            Constants.Namespaces.DomainBuildersExpressions,
            "BuildTyped",
            "Build",
            "Builder"
        );
}
