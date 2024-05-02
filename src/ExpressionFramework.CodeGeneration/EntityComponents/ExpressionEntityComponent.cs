namespace ExpressionFramework.CodeGeneration.EntityComponents;

[ExcludeFromCodeCoverage]
public class ExpressionEntityComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new ExpressionEntityComponent();
}

[ExcludeFromCodeCoverage]
public class ExpressionEntityComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.SourceModel.Namespace == Constants.Namespaces.DomainExpressions)
        {
            context.Request.Builder.AddMethods(new MethodBuilder()
                .WithOverride()
                .WithName("GetSingleContainedExpression")
                .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.TypeNames.Expression}>")
                .AddStringCodeStatements(GetSingleContainedExpressionStatements(context.Request.Builder))
            );
        }

        return Task.FromResult(Result.Continue());
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
}
