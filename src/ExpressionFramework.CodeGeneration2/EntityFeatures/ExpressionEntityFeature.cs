namespace ExpressionFramework.CodeGeneration.EntityFeatures;

[ExcludeFromCodeCoverage]
public class ExpressionEntityFeatureBuilder : IEntityFeatureBuilder
{
    public IPipelineFeature<IConcreteTypeBuilder, EntityContext> Build()
        => new ExpressionEntityFeature();
}

[ExcludeFromCodeCoverage]
public class ExpressionEntityFeature : IPipelineFeature<IConcreteTypeBuilder, EntityContext>
{
    public Result<IConcreteTypeBuilder> Process(PipelineContext<IConcreteTypeBuilder, EntityContext> context)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Context.SourceModel.Namespace == Constants.Namespaces.DomainExpressions)
        {
            context.Model.AddMethods(new MethodBuilder()
                .WithOverride()
                .WithName("GetSingleContainedExpression")
                .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>")
                .AddStringCodeStatements(GetSingleContainedExpressionStatements(context.Model))
            );
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    private static string GetSingleContainedExpressionStatements(IConcreteTypeBuilder typeBaseBuilder)
    {
        var expressionProperties = typeBaseBuilder.Properties
            .Where(x => x.Name == "Expression" && x.TypeName.WithoutProcessedGenerics().GetClassName().In($"I{Constants.Types.Expression}", Constants.Types.ITypedExpression))
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

    public IBuilder<IPipelineFeature<IConcreteTypeBuilder, EntityContext>> ToBuilder()
        => new ExpressionEntityFeatureBuilder();
}
