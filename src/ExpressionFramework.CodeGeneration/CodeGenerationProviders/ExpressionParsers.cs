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
                .AddInterfaces(typeof(IFunctionResultParser))
                .AddMethods(
                    new ClassMethodBuilder()
                        .WithName(nameof(IFunctionResultParser.Parse))
                        .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                        .AddParameter("functionParseResult", typeof(FunctionParseResult))
                        //.AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                        .AddParameter("evaluator", "CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator")
                        .AddLiteralCodeStatements(
                            $"if (functionParseResult.{nameof(FunctionParseResult.FunctionName)}.ToUpperInvariant() != \"{x.Name[..^10].ToUpperInvariant()}\")",
                            "{",
                            $"    return {typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName!.GetCsharpFriendlyTypeName()}?>.Continue();",
                            "}"
                        )
                        .AddNotImplementedException()
                )
                .Build()
            );
}
