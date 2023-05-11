namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class ExpressionVisitor : IVisitor
{
    public void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder, VisitorContext context)
    where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
    where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Namespace.ToString() != Constants.Namespaces.DomainExpressions)
        {
            return;
        }

        typeBaseBuilder.AddMethods(new ClassMethodBuilder()
            .WithOverride()
            .WithName("GetSingleContainedExpression")
            .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>")
            .AddLiteralCodeStatements(GetSingleContainedExpressionStatements(typeBaseBuilder))
        );
    }

    private static string GetSingleContainedExpressionStatements<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    where TBuilder : TypeBaseBuilder<TBuilder, TEntity>
    where TEntity : ITypeBase
    {
        if (typeBaseBuilder.Name.ToString().EndsWith("Base"))
        {
            return $"throw new {typeof(NotSupportedException).FullName}();";
        }

        var expressionProperties = typeBaseBuilder.Properties
            .Where(x => x.TypeName.ToString().WithoutProcessedGenerics().GetClassName().In(Constants.Types.Expression, Constants.Types.ITypedExpression))
            .ToArray();
        if (expressionProperties.Length == 1)
        {
            var nullableTypedPrefix = expressionProperties[0].IsNullable
                ? "?"
                : string.Empty;

            var typedSuffix = expressionProperties[0].TypeName.ToString().WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression
                ? $"{nullableTypedPrefix}.ToUntyped()"
                : string.Empty;

            var nullableSuffix = expressionProperties[0].IsNullable
                ? $" ?? new {Constants.Namespaces.DomainExpressions}.EmptyExpression()"
                : string.Empty;
            
            return $"return {typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>.Success({expressionProperties[0].Name}{typedSuffix}{nullableSuffix});";
        }

        return $"return {typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>.NotFound();";
    }
}
