namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedExpressionVisitor : IVisitor
{
    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() != Constants.Namespaces.DomainExpressions)
        {
            return;
        }

        var key = typeBaseBuilder.GetFullName();
        if (context.TypedInterfaceMap.TryGetValue(key, out var typedInterface))
        {
            typeBaseBuilder.AddInterfaces($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typedInterface.GetGenericArguments()}>");
            if (!typeBaseBuilder.Name.ToString().StartsWithAny("TypedConstant", "TypedDelegate"))
            {
                typeBaseBuilder.AddMethods(
                    new ClassMethodBuilder()
                        .WithName("ToUntyped")
                        .WithTypeName(Constants.Types.Expression)
                        .AddLiteralCodeStatements("return this;")
                );
            }
        }
        else if (key.EndsWith("Base"))
        {
            typeBaseBuilder.AddMethods(
                new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .AddNotImplementedException()
            );

            context.BaseTypes.Add(typeBaseBuilder.GetFullName(), typeBaseBuilder);
        }
    }
}
