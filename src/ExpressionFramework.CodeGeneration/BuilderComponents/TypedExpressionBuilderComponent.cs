namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderComponentBuilder : IBuilderComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public TypedExpressionBuilderComponentBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public IPipelineComponent<BuilderContext> Build()
        => new TypedExpressionBuilderComponent(_formattableStringParser);
}

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderComponent : BuilderComponentBase, IPipelineComponent<BuilderContext>
{
    private string GetExpressionTemplate(Property property) => $"return {{BuilderAddMethodName}}({{NamePascalCsharpFriendlyName}}.Select(x => new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{CreateTypeName(property)}>().WithValue(x)));";

    public TypedExpressionBuilderComponent(IFormattableStringParser formattableStringParser) : base(formattableStringParser)
    {
    }

    public Task<Result> Process(PipelineContext<BuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        foreach (var property in context.Request.GetSourceProperties().Where(context.Request.IsValidForFluentMethod))
        {
            var parentChildContext = new ParentChildContext<PipelineContext<BuilderContext>, Property>(context, property, context.Request.Settings);

            if (!property.TypeName.FixTypeName().IsCollectionTypeName() && property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
            {
                var results = context.Request.GetResultsForBuilderNonCollectionProperties(property, parentChildContext, FormattableStringParser);

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error.Result);
                }

                AddOverloadsForTypedExpression(context, property, results);
            }
            else if (property.TypeName.StartsWith($"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}"))
            {
                var expressionTemplate = GetExpressionTemplate(property);
                var results = context.Request.GetResultsForBuilderCollectionProperties
                (
                    property,
                    parentChildContext,
                    FormattableStringParser,
                    GetCodeStatementsForEnumerableOverload(context, property, parentChildContext, expressionTemplate),
                    GetCodeStatementsForArrayOverload(context, property, parentChildContext, expressionTemplate)
                );

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error.Result);
                }

                AddOverloadsForTypedExpressions(context, property, results);
            }
        }

        return Task.FromResult(Result.Continue());
    }

    private static void AddOverloadsForTypedExpression(PipelineContext<BuilderContext> context, Property property, NamedResult<Result<FormattableStringParserResult>>[] results)
    {
        // Add builder overload that uses T instead of ITypedExpression<T>, and calls the other overload.

        // we need the Value propery of Nullable<T> for value types...
        var suffix = property.IsNullable && property.GenericTypeArguments.Count == 1 && property.GenericTypeArguments.First().IsValueType
            ? ".Value"
            : string.Empty;

        var builder = new MethodBuilder()
            .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
            .WithReturnTypeName(context.Request.IsBuilderForAbstractEntity
                ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
                : $"{results.First(x => x.Name == "Namespace").Result.Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithTypeName(CreateTypeName(property))
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()}{suffix});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()}{suffix});",
            context.Request.ReturnValueStatementForFluentMethod
        );

        context.Request.Builder.AddMethods(builder);

        builder = new MethodBuilder()
            .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
            .WithReturnTypeName(context.Request.IsBuilderForAbstractEntity
                  ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
                  : $"{results.First(x => x.Name == "Namespace").Result.Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(Func<>).WithoutGenerics()}<{typeof(object).FullName}?, {CreateTypeName(property)}>")
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.TypedDelegateExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.TypedDelegateExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
            context.Request.ReturnValueStatementForFluentMethod
        );

        context.Request.Builder.AddMethods(builder);
    }

    private static void AddOverloadsForTypedExpressions(PipelineContext<BuilderContext> context, Property property, NamedResult<Result<FormattableStringParserResult>>[] results)
    {
        var returnType = context.Request.IsBuilderForAbstractEntity
            ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
            : $"{results.First(x => x.Name == "Namespace").Result.Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}";

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{CreateTypeName(property)}>")
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "EnumerableOverload").Select(x => x.Result.Value!.ToString()))
        );

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{CreateTypeName(property)}>".ConvertTypeNameToArray())
                    .WithIsParamArray()
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "ArrayOverload").Select(x => x.Result.Value!.ToString()))
        );
    }

    private static string CreateTypeName(Property property)
    {
        if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            if (property.Name == Constants.ArgumentNames.PredicateExpression)
            {
                // hacking here... we only want to allow to inject the typed expression
                return property.TypeName;
            }
            else
            {
                return property.TypeName.GetGenericArguments();
            }
        }

        if (property.TypeName.GetGenericArguments().StartsWith($"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}"))
        {
            return property.TypeName.GetGenericArguments().GetGenericArguments();
        }

        if (property.TypeName.GetClassName() == Constants.Types.Expression)
        {
            return typeof(object).FullName!;
        }

        return property.TypeName;
    }
}
