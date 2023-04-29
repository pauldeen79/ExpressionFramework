namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class EvaluatableParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IEvaluatable))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .WithBaseClass("EvaluatableParserBase")
                .AddConstructors(new ClassConstructorBuilder()
                    .WithChainCall($"base({x.Name.CsharpFormat()})")
                )
                .AddMethods(new ClassMethodBuilder()
                    .WithName("DoParse")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Evaluatable>")
                    .AddParameter("functionParseResult", typeof(FunctionParseResult))
                    .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                    .WithProtected()
                    .WithOverride()
                    .AddNotImplementedException()
                )
            .Build()
            );
}
