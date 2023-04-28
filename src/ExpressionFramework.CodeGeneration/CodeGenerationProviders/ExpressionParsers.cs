namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserFunctionResultParsers;
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(x => !x.Name.StartsWith("TypedDelegate"))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .WithBaseClass("ExpressionParserBase")
                .AddConstructors(new ClassConstructorBuilder()
                    .AddParameter("parser", typeof(IExpressionParser))
                    .WithChainCall($"base(parser, {x.Name[..^10].CsharpFormat()})")
                )
                .AddMethods(new ClassMethodBuilder()
                    .WithName("DoParse")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Expression>")
                    .AddParameter("functionParseResult", typeof(FunctionParseResult))
                    .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                    .WithProtected()
                    .WithOverride()
                    .AddNotImplementedException()
                )
            .Build()
            );
}
