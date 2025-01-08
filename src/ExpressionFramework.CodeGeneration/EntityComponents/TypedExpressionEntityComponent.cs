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
            && context.Request.SourceModel.Interfaces.Any(x => x.WithoutGenerics() == Constants.TypeNames.TypedExpression))
        {
            if (context.Request.SourceModel.GenericTypeArguments.Count == 0)
            {
                context.Request.Builder.AddMethods(new MethodBuilder()
                    .WithName("ToUntyped")
                    .WithReturnTypeName(Constants.Types.Expression)
                    .AddStringCodeStatements("return this;")
                );
            }

            //quirks for ITypedExpression<T> / ITypedExpressionBuilder<T>
            context.Request.Builder.Methods.First(x => x.Name == context.Request.Settings.ToTypedBuilderFormatString)
                .WithReturnTypeName(Constants.TypeNames.TypedExpressionBuilder.MakeGenericTypeName(context.Request.SourceModel.Interfaces.First(x => x.WithoutGenerics() == Constants.TypeNames.TypedExpression).GetGenericArguments()));

            context.Request.Builder.Methods.First(x => x.Name == context.Request.Settings.ToBuilderFormatString)
                .With(builder =>
                {
                    builder.CodeStatements.Clear();
                    builder.CodeStatements.Add(new StringCodeStatementBuilder().WithStatement($"return ({Constants.TypeNames.ExpressionBuilder})ToTypedBuilder();"));
                });
        }

        return Task.FromResult(Result.Continue());
    }
}
