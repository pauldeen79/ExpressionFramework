namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionHandlers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/ExpressionHandlers";
    public override string DefaultFileName => "ExpressionHandlers.cs";
    
    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideExpressionModels()
        .Where(x => !File.Exists(System.IO.Path.Combine(FullBasePath, Path, $"{x.Name}Handler.cs"))) //this is a quirk, really...
        .Select(x => new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.ExpressionHandlers")
            .WithName($"{x.Name}Handler")
            .WithBaseClass($"ExpressionHandlerBase<{x.Name}>")
            .AddMethods(new ClassMethodBuilder()
                .WithName("Evaluate")
                .WithProtected()
                .WithOverride()
                .AddParameter("context", "System.Object?")
                .AddParameter("typedExpression", "ChainedExpression")
                .AddParameter("evaluator", "IExpressionEvaluator")
                .WithTypeName("Task<Result<object?>>") //this is a quirk, really...
                .AddLiteralCodeStatements("throw new NotImplementedException();")
            )
            .Build());

    private static string FullBasePath => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
        ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
        : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");

}
