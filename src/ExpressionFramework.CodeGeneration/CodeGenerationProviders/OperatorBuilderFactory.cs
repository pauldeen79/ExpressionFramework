namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IOperator)),
            new("ExpressionFramework.Domain.Builders",
            "OperatorBuilderFactory",
            "ExpressionFramework.Domain.Operator",
            "ExpressionFramework.Domain.Builders.Operators",
            "OperatorBuilder",
            "ExpressionFramework.Domain.Operators"));
}
