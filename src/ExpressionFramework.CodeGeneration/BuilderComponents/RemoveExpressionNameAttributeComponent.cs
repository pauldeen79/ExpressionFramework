namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public class RemoveExpressionNameAttributeComponent : IPipelineComponent<BuilderContext>
{
    public Task<Result> ProcessAsync(PipelineContext<BuilderContext> context, CancellationToken token)
    {
        var expressionNameAttribute = context.Request.Builder.Attributes.FirstOrDefault(x => x.Name.GetClassName() == nameof(ExpressionNameAttribute));
        if (expressionNameAttribute is not null)
        {
            context.Request.Builder.Attributes.Remove(expressionNameAttribute);
        }

        return Task.FromResult(Result.Success());
    }
}
