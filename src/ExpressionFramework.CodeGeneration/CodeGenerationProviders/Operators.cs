namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Operators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Operators";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IOperator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithProtected()
                    .WithOverride()
                    .AddParameters
                    (
                        new ParameterBuilder().WithName("leftValue").WithType(typeof(object)).WithIsNullable(),
                        new ParameterBuilder().WithName("rightValue").WithType(typeof(object)).WithIsNullable()
                    )
                    .WithType(typeof(Result<bool>))
                    .AddNotImplementedException()
                )
                .Build());
}
