namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public abstract class BuilderComponentBase
{
    protected IFormattableStringParser FormattableStringParser { get; }

    protected BuilderComponentBase(IFormattableStringParser formattableStringParser)
    {
        FormattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    protected IEnumerable<Result<FormattableStringParserResult>> GetCodeStatementsForEnumerableOverload(PipelineContext<BuilderContext> context, Property property, ParentChildContext<PipelineContext<BuilderContext>, Property> parentChildContext, string expressionTemplate)
    {
        if (context.Request.Settings.BuilderNewCollectionTypeName == typeof(IEnumerable<>).WithoutGenerics())
        {
            // When using IEnumerable<>, do not call ToArray because we want lazy evaluation
            foreach (var statement in GetCodeStatementsForArrayOverload(context, property, parentChildContext, expressionTemplate))
            {
                yield return statement;
            }

            yield break;
        }

        // When not using IEnumerable<>, we can simply force ToArray because it's stored in a generic list or collection of some sort anyway.
        // (in other words, materialization is always performed)
        if (context.Request.Settings.AddNullChecks)
        {
            yield return Result.Success<FormattableStringParserResult>(context.Request.CreateArgumentNullException(property.Name.ToPascalCase(context.Request.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()));
        }

        yield return FormattableStringParser.Parse("return {BuilderAddMethodName}({NamePascalCsharpFriendlyName}.ToArray());", context.Request.FormatProvider, parentChildContext);
    }

    protected IEnumerable<Result<FormattableStringParserResult>> GetCodeStatementsForArrayOverload(PipelineContext<BuilderContext> context, Property property, ParentChildContext<PipelineContext<BuilderContext>, Property> parentChildContext, string expressionTemplate)
    {
        if (context.Request.Settings.AddNullChecks)
        {
            var argumentNullCheckResult = FormattableStringParser.Parse
            (
                context.Request.GetMappingMetadata(property.TypeName).GetStringValue(MetadataNames.CustomBuilderArgumentNullCheckExpression, "{NullCheck.Argument}"),
                context.Request.FormatProvider,
                new ParentChildContext<PipelineContext<BuilderContext>, Property>(context, property, context.Request.Settings)
            );

            if (!argumentNullCheckResult.IsSuccessful() || !string.IsNullOrEmpty(argumentNullCheckResult.Value!))
            {
                yield return argumentNullCheckResult;
            }
        }

        yield return FormattableStringParser.Parse(expressionTemplate, context.Request.FormatProvider, parentChildContext);
    }
}
