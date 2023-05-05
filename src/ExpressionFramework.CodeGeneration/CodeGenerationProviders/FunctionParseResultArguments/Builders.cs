namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public class Builders : FunctionParseResultArgumentsBase
{
    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName == $"{RecordConcreteCollectionType.WithoutGenerics()}<{typeof(FunctionParseResultArgument).FullName}>")
        {
            var init = $"{CurrentNamespace}.{nameof(FunctionParseResultArgumentBuilderFactory)}.Create(x)";
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                RecordConcreteCollectionType.WithoutGenerics(),
                ReplaceWithBuilderNamespaces(typeName).ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                "{0} = source.{0}.Select(x => " + init + ").ToList()",
                builderCollectionTypeName: BuilderClassCollectionType.WithoutGenerics()
            );
        }
        else
        {
            base.FixImmutableBuilderProperty(property, typeName);
        }
    }

    public override object CreateModel()
        => GetImmutableBuilderClasses(new[] { typeof(FunctionParseResult) }, ProjectName, CurrentNamespace);
}
