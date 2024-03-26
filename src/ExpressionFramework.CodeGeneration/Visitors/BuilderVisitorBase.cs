namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public static class BuilderVisitorBase
{
    public static void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context, string ns, string typedMethodName, string untypedMethodName, string builderName)
    where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
    where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace != ns)
        {
            return;
        }

        var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name == typedMethodName);
        if (!context.TypedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.WithoutProcessedGenerics(), out var typedInterface))
        {
            return;
        }

        typeBaseBuilder.AddMethods
        (
            new ClassMethodBuilder()
                .WithName(untypedMethodName)
                .WithTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>")
                .AddLiteralCodeStatements($"return {typedMethodName}();")
                .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}{builderName}<{typedInterface.GetGenericArguments()}>")
        ).AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}{builderName}<{typedInterface.GetGenericArguments()}>");
    }
}
