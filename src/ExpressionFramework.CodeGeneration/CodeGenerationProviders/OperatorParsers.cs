namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorParsersScaffolded : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserOperatorResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IOperator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .WithBaseClass("OperatorParserBase")
                .AddConstructors(new ClassConstructorBuilder()
                    .WithChainCall($"base({x.Name[..^8].CsharpFormat()})")
                )
                .AddMethods(new ClassMethodBuilder()
                    .WithName("DoParse")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Operator>")
                    .AddParameter("functionParseResult", typeof(FunctionParseResult))
                    .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                    .WithProtected()
                    .WithOverride()
                    .AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.Operator>.Success(new {Constants.Namespaces.DomainOperators}.{x.Name}());")
                )
            .Build()
            );
}
