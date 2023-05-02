namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IOperator))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Operator,
                x.Name,
                m => m.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.{Constants.Types.Operator}>.Success(new {Constants.Namespaces.DomainOperators}.{x.Name}());")
            ).Build());
}
