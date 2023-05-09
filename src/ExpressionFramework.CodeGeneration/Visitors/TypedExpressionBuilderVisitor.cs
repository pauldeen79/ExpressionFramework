namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderVisitor : IVisitor
{
    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() != Constants.Namespaces.DomainBuildersExpressions)
        {
            return;
        }

        var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name.ToString() == "BuildTyped");
        if (!context.TypedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.ToString().WithoutProcessedGenerics(), out var typedInterface))
        {
            return;
        }

        typeBaseBuilder.AddMethods
        (
            new ClassMethodBuilder()
                .WithName("Build")
                .WithTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>")
                .AddLiteralCodeStatements("return BuildTyped();")
                .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typedInterface.GetGenericArguments()}>")
        ).AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typedInterface.GetGenericArguments()}>");
    }
}
