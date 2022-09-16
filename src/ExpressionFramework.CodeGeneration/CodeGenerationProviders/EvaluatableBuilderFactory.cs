namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class EvaluatableBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideEvaluatableModels(),
            "ExpressionFramework.Domain.Builders",
            "EvaluatableBuilderFactory",
            "ExpressionFramework.Domain.Evaluatable",
            "ExpressionFramework.Domain.Builders.Evaluatables",
            "EvaluatableBuilder");
}
