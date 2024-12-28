﻿namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderComponentBuilder(IFormattableStringParser formattableStringParser) : IBuilderComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));

    public IPipelineComponent<BuilderContext> Build()
        => new ExpressionBuilderComponent(_formattableStringParser);
}

[ExcludeFromCodeCoverage]
public class ExpressionBuilderComponent(IFormattableStringParser formattableStringParser) : BuilderComponentBase(formattableStringParser), IPipelineComponent<BuilderContext>
{
    private const string ExpressionTemplate = $"return {{$addMethodNameFormatString}}({{CsharpFriendlyName(ToCamelCase($property.Name))}}.Select(x => new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue(x)));";

    public Task<Result> Process(PipelineContext<BuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.SourceModel.Interfaces.Any(x => x.WithoutGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression"))
        {
            var generics = context.Request.SourceModel.Interfaces
                .First(x => x.WithoutGenerics() == "ExpressionFramework.Domain.Contracts.ITypedExpression")
                .GetGenericArguments();

            context.Request.Builder.AddMethods
            (
                new MethodBuilder()
                    .WithName("Build")
                    .WithReturnTypeName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{generics}>")
                    .AddStringCodeStatements($"return BuildTyped();")
                    .WithExplicitInterfaceName($"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{generics}>")
            );
        }

        // Add builder overload with type object/object? that maps to new ConstantEvaluatableBuilder().WithValue(value)
        foreach (var property in context.Request.GetSourceProperties().Where(context.Request.IsValidForFluentMethod))
        {
            var parentChildContext = new ParentChildContext<PipelineContext<BuilderContext>, Property>(context, property, context.Request.Settings);
            if (!property.TypeName.FixTypeName().IsCollectionTypeName() && property.TypeName.GetClassName() == Constants.Types.Expression)
            {
                var results = context.Request.GetResultsForBuilderNonCollectionProperties(property, parentChildContext, FormattableStringParser);

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error.Result);
                }

                AddOverloadsForExpression(context, property, results);
            }
            else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>")
            {
                var results = context.Request.GetResultsForBuilderCollectionProperties
                (
                    property,
                    parentChildContext,
                    FormattableStringParser,
                    GetCodeStatementsForEnumerableOverload(context, property, parentChildContext, ExpressionTemplate),
                    GetCodeStatementsForArrayOverload(context, property, parentChildContext, ExpressionTemplate)
                );

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error.Result);
                }

                AddOverloadsForExpressions(context, property, results);
            }
        }

        return Task.FromResult(Result.Continue());
    }

    private static void AddOverloadsForExpression(PipelineContext<BuilderContext> context, Property property, NamedResult<Result<FormattableStringParserResult>>[] results)
    {
        var builder = new MethodBuilder()
            .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
            .WithReturnTypeName(context.Request.IsBuilderForAbstractEntity
                ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
                : $"{results.First(x => x.Name == "Namespace").Result.Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(object))
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
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
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(Func<>).WithoutGenerics()}<{typeof(object).FullName}?, {typeof(object).FullName}>")
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
            context.Request.ReturnValueStatementForFluentMethod
        );

        context.Request.Builder.AddMethods(builder);
    }

    private static void AddOverloadsForExpressions(PipelineContext<BuilderContext> context, Property property, NamedResult<Result<FormattableStringParserResult>>[] results)
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
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(IEnumerable<object>))
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "EnumerableOverload").Select(x => x.Result.Value!.ToString()))
        );

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(object[]))
                    .WithIsParamArray()
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "ArrayOverload").Select(x => x.Result.Value!.ToString()))
        );
    }
}
