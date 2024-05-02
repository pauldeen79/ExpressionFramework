namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Parsers : ExpressionFrameworkCSharpClassBase
{
    public Parsers(IMediator mediator, ICsharpExpressionDumper csharpExpressionDumper) : base(mediator, csharpExpressionDumper)
    {
    }

    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    public override async Task<IEnumerable<TypeBase>> GetModel()
    {
        var settings = CreateSettings();
        return (await GetOverrideModels(typeof(IOperator)))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Operator,
                x.WithoutInterfacePrefix(),
                Constants.Namespaces.DomainOperators,
                settings
            ).Build());
    }
}
