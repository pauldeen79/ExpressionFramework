namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedExpressionModelVisitor : IVisitor
{
    public int Order => 30;

    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() != Constants.Namespaces.DomainModelsExpressions)
        {
            return;
        }

        var buildTypedMethod = typeBaseBuilder.Methods.First(x => x.Name.ToString() == "ToTypedEntity");
        if (!context.TypedInterfaceMap.TryGetValue(buildTypedMethod.TypeName.ToString().WithoutProcessedGenerics(), out var typedInterface))
        {
            return;
        }

        typeBaseBuilder.AddMethods
        (
            new ClassMethodBuilder()
                .WithName("ToEntity")
                .WithTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>")
                .AddLiteralCodeStatements("return ToTypedEntity();")
                .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Model<{typedInterface.GetGenericArguments()}>")
        ).AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Model<{typedInterface.GetGenericArguments()}>");
    }
}
