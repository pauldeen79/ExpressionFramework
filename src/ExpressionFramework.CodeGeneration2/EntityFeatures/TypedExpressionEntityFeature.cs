namespace ExpressionFramework.CodeGeneration.EntityFeatures;

public class TypedExpressionEntityFeatureBuilder : IEntityFeatureBuilder
{
    public IPipelineFeature<IConcreteTypeBuilder, EntityContext> Build()
        => new TypedExpressionEntityFeature();
}

public class TypedExpressionEntityFeature : IPipelineFeature<IConcreteTypeBuilder, EntityContext>
{
    public Result<IConcreteTypeBuilder> Process(PipelineContext<IConcreteTypeBuilder, EntityContext> context)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Context.SourceModel.Namespace == Constants.Namespaces.DomainExpressions
            && context.Context.SourceModel.Interfaces.Any(x => x.WithoutProcessedGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression")
            && context.Context.SourceModel.GenericTypeArguments.Count == 0)
        {
            context.Model.AddMethods(new MethodBuilder()
                .WithName("ToUntyped")
                .WithReturnTypeName(Constants.Types.Expression)
                .AddStringCodeStatements("return this;")
            );
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    public IBuilder<IPipelineFeature<IConcreteTypeBuilder, EntityContext>> ToBuilder()
        => new TypedExpressionEntityFeatureBuilder();
}
