namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Expressions : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Expressions";
    public override string DefaultFileName => "Expressions.cs";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideExpressionModels()
            .Select(x => new ClassBuilder()
                .WithNamespace("ExpressionFramework.Domain.Expressions")
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddLiteralCodeStatements("throw new NotImplementedException();")
                )
                .Build());
}
