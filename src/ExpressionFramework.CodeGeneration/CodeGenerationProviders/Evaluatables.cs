namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Evaluatables : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Evaluatables";
    public override string DefaultFileName => "Evaluatables.cs";

    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideEvaluatableModels()
            .Where(x => IsNotScaffolded(x, string.Empty))
            .Select(x => new ClassBuilder()
                .WithNamespace("ExpressionFramework.Domain.Evaluatables")
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .WithType(typeof(Result<bool>))
                    .AddLiteralCodeStatements("throw new NotImplementedException();")
                )
                .Build());
}
