namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Aggregators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecialized}/{nameof(Aggregators)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specialized", string.Empty);

    public override object CreateModel()
        => GetOverrideModels(typeof(Models.IAggregator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Aggregate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .AddParameter("secondExpression", GetModelTypeName(typeof(IExpression)))
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddNotImplementedException()
                )
                .Build());

}
