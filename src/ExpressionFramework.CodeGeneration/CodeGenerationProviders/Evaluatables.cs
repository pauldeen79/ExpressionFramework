namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Evaluatables : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/{nameof(Evaluatables)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    
    public override object CreateModel()
        => GetOverrideModels(typeof(IEvaluatable))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .WithType(typeof(Result<bool>))
                    .AddNotImplementedException()
                )
                .Build());
}
