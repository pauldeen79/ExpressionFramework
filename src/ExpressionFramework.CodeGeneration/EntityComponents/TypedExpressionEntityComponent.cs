namespace ExpressionFramework.CodeGeneration.EntityComponents;

[ExcludeFromCodeCoverage]
public class TypedExpressionEntityComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<IConcreteTypeBuilder, EntityContext> Build()
        => new TypedExpressionEntityComponent();
}

[ExcludeFromCodeCoverage]
public class TypedExpressionEntityComponent : IPipelineComponent<IConcreteTypeBuilder, EntityContext>
{
    public Task<Result<IConcreteTypeBuilder>> Process(PipelineContext<IConcreteTypeBuilder, EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Context.SourceModel.Namespace == Constants.Namespaces.DomainExpressions
            && context.Context.SourceModel.Interfaces.Any(x => x.WithoutProcessedGenerics() == Constants.TypeNames.TypedExpression)
            && context.Context.SourceModel.GenericTypeArguments.Count == 0)
        {
            context.Model.AddMethods(new MethodBuilder()
                .WithName("ToUntyped")
                .WithReturnTypeName(Constants.Types.Expression)
                .AddStringCodeStatements("return this;")
            );
        }

        return Task.FromResult(Result.Continue<IConcreteTypeBuilder>());
    }
}
