namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Expressions;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IMediator mediator, ICsharpExpressionDumper csharpExpressionDumper) : base(mediator, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    public override async Task<IEnumerable<TypeBase>> GetModel()
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IExpression)))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Expression,
                x.WithoutInterfacePrefix().ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                Constants.Namespaces.DomainExpressions,
                settings
            ).Build());
    }
}
