namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    public override object CreateModel()
        => GetOverrideModels(typeof(IOperator))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Operator,
                x.Name,
                Constants.Namespaces.DomainOperators
            ).Build());
}
