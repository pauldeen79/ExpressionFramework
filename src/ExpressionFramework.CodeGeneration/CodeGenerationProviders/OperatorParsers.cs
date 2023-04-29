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
                "Operator",
                x.Name,
                false,
                m => m.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.Operator>.Success(new {Constants.Namespaces.DomainOperators}.{x.Name}());")
            ).Build());
}
