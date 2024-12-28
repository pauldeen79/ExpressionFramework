namespace ExpressionFramework.CodeGeneration.EntityComponents;

[ExcludeFromCodeCoverage]
public class TypedExpressionEntityComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new TypedExpressionEntityComponent();
}

[ExcludeFromCodeCoverage]
public class TypedExpressionEntityComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.SourceModel.Namespace == Constants.Namespaces.DomainExpressions
            && context.Request.SourceModel.Interfaces.Any(x => x.WithoutGenerics() == Constants.TypeNames.TypedExpression)
            && context.Request.SourceModel.GenericTypeArguments.Count == 0)
        {
            context.Request.Builder.AddMethods(new MethodBuilder()
                .WithName("ToUntyped")
                .WithReturnTypeName(Constants.Types.Expression)
                .AddStringCodeStatements("return this;")
            );
        }

        return Task.FromResult(Result.Continue());
    }
}
