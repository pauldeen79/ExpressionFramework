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
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .AddInterfaces(typeof(IFunctionResultParser))
                .AddMethods(
                    new ClassMethodBuilder()
                        .WithName(nameof(IFunctionResultParser.Parse))
                        .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                        .AddParameter("functionParseResult", typeof(FunctionParseResult))
                        .AddNotImplementedException()
                )
                .Build()
            );
}
