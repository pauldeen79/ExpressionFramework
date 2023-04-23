namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Aggregators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecific}/{nameof(Aggregators)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specific", string.Empty);

    public override object CreateModel()
        => GetOverrideModels(typeof(IAggregator))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Aggregate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .AddParameters(new ParameterBuilder().WithName("secondExpression").WithTypeName(GetModelTypeName(typeof(IExpression))))
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddNotImplementedException()
                )
                .Build());

}
