namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Aggregators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Aggregators";
    public override string DefaultFileName => "Aggregators.cs";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IAggregator))
            .Select(x => new ClassBuilder()
                .WithNamespace("ExpressionFramework.Domain.Aggregators")
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Aggregate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .AddParameters(new ParameterBuilder().WithName("secondExpression").WithTypeName("ExpressionFramework.Domain.Expression"))
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddNotImplementedException()
                )
                .Build());

}
