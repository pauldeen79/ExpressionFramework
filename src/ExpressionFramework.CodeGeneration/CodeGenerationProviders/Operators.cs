namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Operators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecialized}/{nameof(Operators)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specialized", string.Empty);

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
                    .AddParameter("leftValue", typeof(object), isNullable: true)
                    .AddParameter("rightValue", typeof(object), isNullable: true)
                    .WithType(typeof(Result<bool>))
                    .AddNotImplementedException()
                )
                .Build());
}
