namespace ExpressionFramework.CodeGeneration.BuilderFeatures;

public class ExpressionBuilderBuilderFeatureBuilder : IBuilderFeatureBuilder
{
    public IPipelineFeature<IConcreteTypeBuilder, BuilderContext> Build()
        => new ExpressionBuilderBuilderFeature();
}

public class ExpressionBuilderBuilderFeature : IPipelineFeature<IConcreteTypeBuilder, BuilderContext>
{
    public Result<IConcreteTypeBuilder> Process(PipelineContext<IConcreteTypeBuilder, BuilderContext> context)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Context.SourceModel.Namespace == Constants.Namespaces.DomainExpressions
            && context.Context.SourceModel.Interfaces.Any(x => x.WithoutProcessedGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression"))
        {
            var generics = context.Context.SourceModel.Interfaces
                .First(x => x.WithoutProcessedGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression")
                .GetGenericArguments();

            context.Model.AddMethods
            (
                new MethodBuilder()
                    .WithName("Build")
                    .WithReturnTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{generics}>")
                    .AddStringCodeStatements($"return BuildTyped();")
                    .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{generics}>")
            );
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    public IBuilder<IPipelineFeature<IConcreteTypeBuilder, BuilderContext>> ToBuilder()
        => new ExpressionBuilderBuilderFeatureBuilder();
}
