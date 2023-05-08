namespace ExpressionFramework.CodeGeneration.Visitors;

public static class TypedExpressionBuilderVisitor
{
    internal static void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() == Constants.Namespaces.DomainBuildersExpressions)
        {
            AddCodeForTypedExpressionToExpressionBuilders(typeBaseBuilder);
        }
    }

    private static void AddCodeForTypedExpressionToExpressionBuilders<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name.ToString() == "BuildTyped");
        if (!VisitorState.TypedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.ToString().WithoutProcessedGenerics(), out var typedInterface))
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
