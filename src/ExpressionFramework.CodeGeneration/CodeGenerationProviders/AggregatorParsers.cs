namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AggregatorParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserAggregatorResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(Models.IAggregator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .WithBaseClass("AggregatorParserBase")
                .AddConstructors(new ClassConstructorBuilder()
                    .WithChainCall($"base({x.Name[..^10].CsharpFormat()})")
                )
                .AddMethods(new ClassMethodBuilder()
                    .WithName("DoParse")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Aggregator>")
                    .AddParameter("functionParseResult", typeof(FunctionParseResult))
                    .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                    .WithProtected()
                    .WithOverride()
                    .AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.Aggregator>.Success(new {Constants.Namespaces.DomainAggregators}.{x.Name}());")
                )
            .Build()
            );
}
