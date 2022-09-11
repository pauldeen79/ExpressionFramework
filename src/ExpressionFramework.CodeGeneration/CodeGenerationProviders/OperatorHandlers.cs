namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorHandlers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/OperatorHandlers";
    public override string DefaultFileName => "OperatorHandlers.cs";
    
    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideOperatorModels()
        .Where(x => !File.Exists(System.IO.Path.Combine(FullBasePath, Path, $"{x.Name}Handler.cs"))) //this is a quirk, really...
        .Select(x => new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.OperatorHandlers")
            .WithName($"{x.Name}Handler")
            .WithBaseClass($"OperatorHandlerBase<{x.Name}>")
            .AddMethods(new ClassMethodBuilder()
                .WithName("Handle")
                .WithProtected()
                .WithOverride()
                .AddParameter("leftValue", "System.Object?")
                .AddParameter("rightValue", "System.Object?")
                .WithType(typeof(bool))
                .AddLiteralCodeStatements("throw new NotImplementedException();")
            )
            .Build());

    private static string FullBasePath => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
        ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
        : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");

}
