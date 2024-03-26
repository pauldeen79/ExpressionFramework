namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class ExpressionVisitor : IVisitor
{
    public int Order => 40;

    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
    where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
    where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace != Constants.Namespaces.DomainExpressions)
        {
            return;
        }

        var key = $"{typeBaseBuilder.GetFullName()}Base";
        context.BaseTypes.TryGetValue(key, out var baseBuilder);
        if (baseBuilder != null)
        {
            typeBaseBuilder.AddMethods(new ClassMethodBuilder()
                .WithOverride()
                .WithName("GetSingleContainedExpression")
                .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>")
                .AddLiteralCodeStatements(GetSingleContainedExpressionStatements(baseBuilder))
            );
        }
        else if (typeBaseBuilder.GetFullName().EndsWith("Base"))
        {
            typeBaseBuilder.AddMethods(new ClassMethodBuilder()
                .WithOverride()
                .WithName("GetSingleContainedExpression")
                .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>")
                .AddLiteralCodeStatements($"throw new {typeof(NotSupportedException).FullName}();")
            );
        }
    }

    private static string GetSingleContainedExpressionStatements(TypeBaseBuilder typeBaseBuilder)
    {
        var expressionProperties = typeBaseBuilder.Properties
            .Where(x => x.Name == "Expression" && x.TypeName.WithoutProcessedGenerics().GetClassName().In(Constants.Types.Expression, Constants.Types.ITypedExpression))
            .ToArray();

        if (expressionProperties.Length == 1)
        {
            var nullableTypedPrefix = expressionProperties[0].IsNullable
                ? "?"
                : string.Empty;

            var typedSuffix = expressionProperties[0].TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression
                ? $"{nullableTypedPrefix}.ToUntyped()"
                : string.Empty;

            var nullableSuffix = expressionProperties[0].IsNullable
                ? $" ?? new {Constants.Namespaces.DomainExpressions}.EmptyExpression()"
                : string.Empty;
            
            return $"return {typeof(Result<>).WithoutGenerics()}.Success({expressionProperties[0].Name}{typedSuffix}{nullableSuffix});";
        }

        return $"return {typeof(Result<>).WithoutGenerics()}.NotFound<{Constants.TypeNames.Expression}>();";
    }
}
