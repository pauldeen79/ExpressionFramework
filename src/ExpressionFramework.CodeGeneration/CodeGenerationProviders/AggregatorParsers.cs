namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AggregatorParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(Models.IAggregator))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Aggregator,
                x.Name,
                false,
                m => m.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.{Constants.Types.Aggregator}>.Success(new {Constants.Namespaces.DomainAggregators}.{x.Name}());")
            ).Build());
}
