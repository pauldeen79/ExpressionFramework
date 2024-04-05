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
        foreach (var property in context.Context.GetSourceProperties().Where(context.Context.IsValidForFluentMethod))
        {
            var parentChildContext = new ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property>(context, property, context.Context.Settings);
            if (!property.TypeName.FixTypeName().IsCollectionTypeName())
            {
                var results = context.Context.GetResultsForBuilderNonCollectionProperties(property, parentChildContext, _formattableStringParser);

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Result.FromExistingResult<IConcreteTypeBuilder>(error.Result);
                }

                if (property.TypeName.GetClassName() == Constants.Types.Expression)
                {
                    AddOverloadsForExpression(context, property, results);
                }
            }
            else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>")
            {
                var results = context.Context.GetResultsForBuilderCollectionProperties
                (
                    property,
                    parentChildContext,
                    _formattableStringParser,
                    GetCodeStatementsForEnumerableOverload(context, property, parentChildContext),
                    GetCodeStatementsForArrayOverload(context, property, parentChildContext)
                );

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Result.FromExistingResult<IConcreteTypeBuilder>(error.Result);
                }

                AddOverloadsForExpressions(context, property, results);
            }
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    private IEnumerable<Result<string>> GetCodeStatementsForEnumerableOverload(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property> parentChildContext)
    {
        if (context.Context.Settings.BuilderNewCollectionTypeName == typeof(IEnumerable<>).WithoutGenerics())
        {
            // When using IEnumerable<>, do not call ToArray because we want lazy evaluation
            foreach (var statement in GetCodeStatementsForArrayOverload(context, property, parentChildContext))
            {
                yield return statement;
            }

            yield break;
        }

        // When not using IEnumerable<>, we can simply force ToArray because it's stored in a generic list or collection of some sort anyway.
        // (in other words, materialization is always performed)
        if (context.Context.Settings.AddNullChecks)
        {
            yield return Result.Success(context.Context.CreateArgumentNullException(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()));
        }

        yield return _formattableStringParser.Parse("return {BuilderAddMethodName}({NamePascalCsharpFriendlyName}.ToArray());", context.Context.FormatProvider, parentChildContext);
    }

    private IEnumerable<Result<string>> GetCodeStatementsForArrayOverload(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property> parentChildContext)
    {
        if (context.Context.Settings.AddNullChecks)
        {
            var argumentNullCheckResult = _formattableStringParser.Parse
            (
                context.Context.GetMappingMetadata(property.TypeName).GetStringValue(MetadataNames.CustomBuilderArgumentNullCheckExpression, "{NullCheck.Argument}"),
                context.Context.FormatProvider,
                new ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property>(context, property, context.Context.Settings)
            );

            if (!string.IsNullOrEmpty(argumentNullCheckResult.Value) || !argumentNullCheckResult.IsSuccessful())
            {
                yield return argumentNullCheckResult;
            }

            if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>")
            {
                yield return _formattableStringParser.Parse($"return {{BuilderAddMethodName}}({{NamePascalCsharpFriendlyName}}.Select(x => new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue(x)));", context.Context.FormatProvider, parentChildContext);
            }
            else
            {
                yield return Result.NotSupported<string>($"Unsupported property typename: {property.TypeName}");
            }
        }
    }

    private static void AddOverloadsForExpression(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, NamedResult<Result<string>>[] results)
    {
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

    private static void AddOverloadsForExpressions(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, NamedResult<Result<string>>[] results)
    {
        var returnType = context.Context.IsBuilderForAbstractEntity
                            ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
                            : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}";

        context.Model.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(IEnumerable<object>))
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "EnumerableOverload").Select(x => x.Result.Value!))
        );

        context.Model.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(object[]))
                    .WithIsParamArray()
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "ArrayOverload").Select(x => x.Result.Value!))
        );
    }

    public IBuilder<IPipelineFeature<IConcreteTypeBuilder, BuilderContext>> ToBuilder()
        => new ExpressionBuilderBuilderFeatureBuilder(_formattableStringParser);
}
