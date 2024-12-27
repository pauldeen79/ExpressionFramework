﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : base(pipelineService, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IExpression)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => CreateParserClass
                (
                    x,
                    Constants.Types.Expression,
                    x.WithoutInterfacePrefix().ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                    Constants.Namespaces.DomainExpressions,
                    settings
                ).Build())));
    }
}
