namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class TypedInterfaceVisitor : IVisitor
{
    public int Order => 20;

    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var typedInterface = GetTypedInterface(typeBaseBuilder);
        if (string.IsNullOrEmpty(typedInterface))
        {
            return;
        }

        var key = typeBaseBuilder.GetFullName();
        if (!context.TypedInterfaceMap.ContainsKey(key))
        {
            context.TypedInterfaceMap.Add(key, typedInterface);
        }
    }

    private static string? GetTypedInterface<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        var typedInterface = typeBaseBuilder.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics());

        // This is a kind of hack for the fact that .net says the generic type argument of IEnumerable<T> is nullable.
        // ModelFramework is not extendable for this, so we are currently hacking this here.
        // Maybe it's an idea to add some sort of formatting function to CodeGenerationSettings, or even try to do this in the type formatting delegate that's already there? 
        if (typedInterface == $"{Constants.CodeGenerationRootNamespace}.Models.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeof(IEnumerable<>).WithoutGenerics()}<{typeof(object).FullName}>>")
        {
            typedInterface = $"{Constants.CodeGenerationRootNamespace}.Models.Contracts.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeof(IEnumerable<>).WithoutGenerics()}<{typeof(object).FullName}?>>";
        }

        return typedInterface;
    }
}
