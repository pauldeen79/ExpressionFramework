namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public static class TypedExpressionVisitor
{
    public static void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
        where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() == Constants.Namespaces.DomainExpressions)
        {
            AddCodeForTypedExpressionToExpressions(typeBaseBuilder);
        }
    }

    private static void AddCodeForTypedExpressionToExpressions<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
    where TEntity : ITypeBase
    {
        var key = typeBaseBuilder.GetFullName();
        if (VisitorState.TypedInterfaceMap.TryGetValue(key, out var typedInterface))
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

            VisitorState.BaseTypes.Add(typeBaseBuilder.GetFullName(), typeBaseBuilder);
        }

        if (!typeBaseBuilder.Name.ToString().EndsWith("Base")
            && typeBaseBuilder is ClassBuilder classBuilder
            && classBuilder.Constructors.Any()
            && VisitorState.BaseTypes.TryGetValue($"{typeBaseBuilder.GetFullName()}Base", out var baseType)
            && baseType.Properties.Any(
                x => x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName()
                  || x.TypeName.ToString().GetClassName() == Constants.Types.Expression)
            )
        {
            // Add c'tor that uses T instead of ITypedExpression<T>, and calls the other overload.
            // This is needed pre .NET 7.0 because we can't use static implicit operators with generics.
            var ctor = classBuilder.Constructors.Last();
            classBuilder.AddConstructors(
                new ClassConstructorBuilder()
                    .AddParameters(
                        ctor.Parameters.Select(x => new ParameterBuilder()
                            .WithName(x.Name)
                            .WithTypeName(CreateTypeName(x))
                            .WithIsNullable(x.IsNullable)
                            .WithDefaultValue(x.DefaultValue)
                            )
                    )
                    .WithChainCall("this(" + string.Join(", ", ctor.Parameters.Select(CreateParameterSelection)) + ")")
                );
        }
    }

    private static string CreateParameterSelection(ParameterBuilder x)
    {
        if (x.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName() && x.Name.ToString() != Constants.ArgumentNames.PredicateExpression)
        {
            // we need the Value propery of Nullable<T> for value types... (except for predicate expressions, those still have to be injected using ITypedExpression<bool>)
            // for now, we only support int, long and boolean
            var suffix = x.TypeName.ToString().GetGenericArguments().In("System.Int32", "System.Int64", "System.Boolean", "int", "long", "bool")
                ? ".Value"
                : string.Empty;

            return x.IsNullable
                ? $"{x.Name.ToString().GetCsharpFriendlyName()} == null ? null : new {Constants.TypeNames.Expressions.TypedConstantExpression}<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()}{suffix})"
                : $"new {Constants.TypeNames.Expressions.TypedConstantExpression}<{x.TypeName.ToString().GetGenericArguments()}>({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        if (x.TypeName.ToString().GetClassName() == Constants.Types.Expression)
        {
            return $"new {Constants.TypeNames.Expressions.ConstantExpression}({x.Name.ToString().GetCsharpFriendlyName()})";
        }

        return x.Name.ToString().GetCsharpFriendlyName();
    }

    private static string CreateTypeName(ParameterBuilder builder)
    {
        if (builder.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            if (builder.Name.ToString() == Constants.ArgumentNames.PredicateExpression)
            {
                // hacking here... we only want to allow to inject the typed expression
                return builder.TypeName.ToString();
            }
            else
            {
                return builder.TypeName.ToString().GetGenericArguments();
            }
        }

        if (builder.TypeName.ToString().GetClassName() == Constants.Types.Expression)
        {
            // note that you might expect to check for the nullability of the property, but the Expression itself may be required although it's evaluation can result in null
            return $"{typeof(object).FullName}?";
        }

        return builder.TypeName.ToString();
    }
}
