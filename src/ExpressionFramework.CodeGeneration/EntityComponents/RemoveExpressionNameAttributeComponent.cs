namespace ExpressionFramework.CodeGeneration.EntityComponents;

[ExcludeFromCodeCoverage]
public class RemoveExpressionNameAttributeComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new RemoveExpressionNameAttributeComponent();
}

[ExcludeFromCodeCoverage]
public class RemoveExpressionNameAttributeComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        var expressionNameAttribute = context.Request.Builder.Attributes.FirstOrDefault(x => x.Name.GetClassName() == nameof(ExpressionNameAttribute));
        if (expressionNameAttribute is not null)
        {
            context.Request.Builder.Attributes.Remove(expressionNameAttribute);
        }

        return Task.FromResult(Result.Success());
    }
}
