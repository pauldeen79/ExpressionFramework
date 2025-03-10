﻿namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderComponent(IFormattableStringParser formattableStringParser) : BuilderComponentBase(formattableStringParser), IPipelineComponent<BuilderContext>
{
    private const string ExpressionTemplate = $"return {{$addMethodNameFormatString}}({{CsharpFriendlyName(ToCamelCase($property.Name))}}.Select(x => new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue(x)));";

    public Task<Result> ProcessAsync(PipelineContext<BuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (context.Request.SourceModel.Interfaces.Any(x => x.WithoutGenerics() == Constants.TypeNames.TypedExpression))
        {
            var generics = context.Request.SourceModel.Interfaces
                .First(x => x.WithoutGenerics() == Constants.TypeNames.TypedExpression)
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

                var error = results.GetError();
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error);
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

                var error = results.GetError();
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Task.FromResult<Result>(error);
                }

                AddOverloadsForExpressions(context, property, results);
            }
        }

        //quirks for ITypedExpression<T> / ITypedExpressionBuilder<T>
        context.Request.Builder.Constructors.First(x => x.Parameters.Count == 1)
            .With(ctorMethod =>
            {
                FixSingleProperty(context, ctorMethod);
                FixCollectionProperty(context, ctorMethod);
            });

        return Task.FromResult(Result.Continue());
    }

    private static void FixCollectionProperty(PipelineContext<BuilderContext> context, ConstructorBuilder ctorMethod)
    {
        foreach (var statement in ctorMethod.CodeStatements.OfType<StringCodeStatementBuilder>().Where(x => x.Statement.Contains(".Select(x => x.ToBuilder()))", StringComparison.Ordinal)))
        {
            var sourceIndex = statement.Statement.LastIndexOf("source.", StringComparison.Ordinal);
            if (sourceIndex == -1)
            {
                continue;
            }
            var selectIndex = statement.Statement.IndexOf(".Select", sourceIndex, StringComparison.Ordinal);
            if (selectIndex == -1)
            {
                continue;
            }
            var propertyName = statement.Statement.Substring(sourceIndex + 7, selectIndex - sourceIndex - 7);
            var property = context.Request.Builder.Properties.FirstOrDefault(x => x.Name == propertyName);
            if (property is null)
            {
                continue;
            }
            if (property.TypeName.GetGenericArguments().StartsWith("ExpressionFramework.Domain.Contracts.ITypedExpressionBuilder"))
            {
                statement.Statement = statement.Statement.Replace(".Select(x => x.ToBuilder()))", ".Select(x => x.ToTypedBuilder()))", StringComparison.Ordinal);
            }
        }
    }

    private static void FixSingleProperty(PipelineContext<BuilderContext> context, ConstructorBuilder ctorMethod)
    {
        foreach (var statement in ctorMethod.CodeStatements.OfType<StringCodeStatementBuilder>().Where(x => x.Statement.StartsWith('_')))
        {
            var index = statement.Statement.IndexOf(" = ");
            if (index == -1)
            {
                continue;
            }
            var propertyName = statement.Statement.Substring(1, index - 1).ToPascalCase(context.Request.FormatProvider.ToCultureInfo());
            var property = context.Request.Builder.Properties.FirstOrDefault(x => x.Name == propertyName);
            if (property is null)
            {
                continue;
            }
            if (property.TypeName.StartsWith("ExpressionFramework.Domain.Contracts.ITypedExpressionBuilder"))
            {
                var nullPrefix = property.IsNullable
                    ? "?"
                    : string.Empty;

                var nullSuffix = statement.Statement.EndsWith("!;")
                    ? "!"
                    : string.Empty;

                statement.Statement = $"_{propertyName.ToCamelCase(context.Request.FormatProvider.ToCultureInfo())} = source.{propertyName}{nullPrefix}.ToTypedBuilder(){nullSuffix};";
            }
        }
    }

    private static void AddOverloadsForExpression(PipelineContext<BuilderContext> context, Property property, Dictionary<string, Result<GenericFormattableString>> results)
    {
        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results["MethodName"].Value!)
            .WithReturnTypeName(context.Request.IsBuilderForAbstractEntity
                ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
                : $"{results["Namespace"].Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results["BuilderName"].Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(object))
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            )
            .AddStringCodeStatements
            (
                property.IsNullable
                    ? $"{property.Name} = {property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                    : $"{property.Name} = new {Constants.TypeNames.Expressions.ConstantExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
                context.Request.ReturnValueStatementForFluentMethod
            ));

        context.Request.Builder.AddMethods(
            new MethodBuilder()
                .WithName(results["MethodName"].Value!)
                .WithReturnTypeName(CreateReturnTypeName(context, results))
                .AddParameters(CreateParameter(context, property))
                .AddStringCodeStatements
                (
                    property.IsNullable
                        ? $"{property.Name} = {property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                        : $"{property.Name} = new {Constants.TypeNames.Expressions.DelegateExpression}Builder().WithValue({property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
                    context.Request.ReturnValueStatementForFluentMethod
                ));
    }

    private static ParameterBuilder CreateParameter(PipelineContext<BuilderContext> context, Property property)
        => new ParameterBuilder()
            .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
            .WithTypeName($"{typeof(Func<>).WithoutGenerics()}<{typeof(object).FullName}?, {typeof(object).FullName}>")
            .WithIsNullable(property.IsNullable)
            .WithDefaultValue(context.Request.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null));

    private static string CreateReturnTypeName(PipelineContext<BuilderContext> context, Dictionary<string, Result<GenericFormattableString>> results)
        => context.Request.IsBuilderForAbstractEntity
            ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
            : $"{results["Namespace"].Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results["BuilderName"].Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}";

    private static void AddOverloadsForExpressions(PipelineContext<BuilderContext> context, Property property, Dictionary<string, Result<GenericFormattableString>> results)
    {
        var returnType = context.Request.IsBuilderForAbstractEntity
            ? $"TBuilder{context.Request.SourceModel.GetGenericTypeArgumentsString()}"
            : $"{results["Namespace"].Value!.ToString().AppendWhenNotNullOrEmpty(".")}{results["BuilderName"].Value}{context.Request.SourceModel.GetGenericTypeArgumentsString()}";

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results["AddMethodName"].Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(IEnumerable<object>))
            )
            .AddStringCodeStatements(results.Where(x => x.Key.StartsWith("EnumerableOverload")).Select(x => x.Value.Value!.ToString()))
        );

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName(results["AddMethodName"].Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToCamelCase(context.Request.FormatProvider.ToCultureInfo()))
                    .WithType(typeof(object[]))
                    .WithIsParamArray()
            )
            .AddStringCodeStatements(results.Where(x => x.Key.StartsWith("ArrayOverload")).Select(x => x.Value.Value!.ToString()))
        );
    }
}
