namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => CreateBuilderFactoryModels
        (
            GetOverrideOperatorModels(),
            "ExpressionFramework.Domain.Tests.Support.Builders",
            "OperatorBuilderFactory",
            "ExpressionFramework.Domain.Operator",
            "ExpressionFramework.Domain.Tests.Support.Builders.Operators",
            "OperatorBuilder"
        );

}
