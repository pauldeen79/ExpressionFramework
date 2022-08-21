namespace CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreBuilders : ExpressionFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "ExpressionFramework.Core/DomainModel/Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        => GetImmutableBuilderClasses
        (
            GetCoreModels(),
            "ExpressionFramework.Core.DomainModel",
            "ExpressionFramework.Core.DomainModel.Builders",
            "ExpressionFramework.Abstractions.DomainModel.Builders.I{0}"
        )
        .Select
        (
            x => new ClassBuilder(x)
                .With(y =>
                {
                    if (y.Interfaces[0].EndsWith("ExpressionBuilder"))
                    {
                        y.Interfaces[0] = "ExpressionFramework.Abstractions.DomainModel.Builders.IExpressionBuilder";
                        y.Methods.Single(z => z.Name == "Build").TypeName = "ExpressionFramework.Abstractions.DomainModel.IExpression";
                    }
                    else if (y.Interfaces[0].EndsWith("CompositeFunctionBuilder"))
                    {
                        y.Interfaces[0] = "ExpressionFramework.Abstractions.DomainModel.Builders.ICompositeFunctionBuilder";
                        y.Methods.Single(z => z.Name == "Build").TypeName = "ExpressionFramework.Abstractions.DomainModel.ICompositeFunction";
                    }
                })
                .Build()
        )
        .ToArray();
}
