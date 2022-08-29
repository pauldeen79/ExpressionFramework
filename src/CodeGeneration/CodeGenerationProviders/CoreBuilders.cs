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
        .Cast<IClass>()
        .Select
        (
            x => new ClassBuilder(x)
                .With(y =>
                {
                    if (y.Interfaces[0].EndsWithAny(StringComparison.InvariantCulture, CustomBuilderTypes.Select(x => $"{x}Builder")))
                    {
                        var className = GetClassName(y.Name);
                        y.Interfaces[0] = $"ExpressionFramework.Abstractions.DomainModel.Builders.I{className}Builder";
                        y.Methods.Single(z => z.Name == "Build").TypeName = $"ExpressionFramework.Abstractions.DomainModel.I{className}";
                    }
                })
                .Build()
        )
        .ToArray();

    private static string GetClassName(string className) // simplifies inherited types to base type, e.g. EmptyExpressionBuilder -> Expression. Note that className ends with 'Builder' here, because we're generating builders!
        => CustomBuilderTypes.FirstOrDefault(x => className.EndsWith($"{x}Builder", StringComparison.InvariantCulture))
        ?? throw new NotSupportedException($"Unsupported type: [{className}]");
}
