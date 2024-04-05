namespace ExpressionFramework.CodeGeneration.BuilderFeatures;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderBuilderFeatureBuilder : IBuilderFeatureBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public ExpressionBuilderBuilderFeatureBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }


    public IPipelineFeature<IConcreteTypeBuilder, BuilderContext> Build()
        => new ExpressionBuilderBuilderFeature(_formattableStringParser);
}

[ExcludeFromCodeCoverage] 
public class ExpressionBuilderBuilderFeature : IPipelineFeature<IConcreteTypeBuilder, BuilderContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public ExpressionBuilderBuilderFeature(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Result<IConcreteTypeBuilder> Process(PipelineContext<IConcreteTypeBuilder, BuilderContext> context)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Context.SourceModel.Namespace != Constants.Namespaces.DomainExpressions)
        {
            return Result.Continue<IConcreteTypeBuilder>();
        }

        if (context.Context.SourceModel.Interfaces.Any(x => x.WithoutProcessedGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression"))
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

        // Add builder overload with type object/object? that maps to new ConstantEvaluatableBuilder().WithValue(value)
        foreach (var property in context.Context.GetSourceProperties().Where(x => context.Context.IsValidForFluentMethod(x) && !x.TypeName.FixTypeName().IsCollectionTypeName() && x.TypeName.GetClassName() == Constants.Types.Expression))
        {
            var parentChildContext = new ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property>(context, property, context.Context.Settings);

            var results = context.Context.GetResultsForBuilderNonCollectionProperties(property, parentChildContext, _formattableStringParser);

            var error = Array.Find(results, x => !x.Result.IsSuccessful());
            if (error is not null)
            {
                // Error in formattable string parsing
                return Result.FromExistingResult<IConcreteTypeBuilder>(error.Result);
            }

            var builder = new MethodBuilder()
                .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
                .WithReturnTypeName(context.Context.IsBuilderForAbstractEntity
                      ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
                      : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}")
                .AddParameters
                (
                    new ParameterBuilder()
                        .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                        .WithType(typeof(object))
                        .WithIsNullable(property.IsNullable)
                        .WithDefaultValue(context.Context.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
                );

            if (context.Context.Settings.AddNullChecks)
            {
                var nullCheckStatement = results.First(x => x.Name == "ArgumentNullCheck").Result.Value!;
                if (!string.IsNullOrEmpty(nullCheckStatement))
                {
                    builder.AddStringCodeStatements(nullCheckStatement);
                }
            }

            builder.AddStringCodeStatements
            (
                property.IsNullable
                    ? $"{property.Name} = {property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                    : $"{property.Name} = new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
                context.Context.ReturnValueStatementForFluentMethod
            );

            context.Model.AddMethods(builder);

            builder = new MethodBuilder()
                .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
                .WithReturnTypeName(context.Context.IsBuilderForAbstractEntity
                      ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
                      : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}")
                .AddParameters
                (
                    new ParameterBuilder()
                        .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                        .WithTypeName($"{typeof(Func<>).WithoutGenerics()}<{typeof(object).FullName}?, {typeof(object).FullName}>")
                        .WithIsNullable(property.IsNullable)
                        .WithDefaultValue(context.Context.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
                );

            if (context.Context.Settings.AddNullChecks)
            {
                var nullCheckStatement = results.First(x => x.Name == "ArgumentNullCheck").Result.Value!;
                if (!string.IsNullOrEmpty(nullCheckStatement))
                {
                    builder.AddStringCodeStatements(nullCheckStatement);
                }
            }

            builder.AddStringCodeStatements
            (
                property.IsNullable
                    ? $"{property.Name} = {property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                    : $"{property.Name} = new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
                context.Context.ReturnValueStatementForFluentMethod
            );

            context.Model.AddMethods(builder);
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    public IBuilder<IPipelineFeature<IConcreteTypeBuilder, BuilderContext>> ToBuilder()
        => new ExpressionBuilderBuilderFeatureBuilder(_formattableStringParser);
}
