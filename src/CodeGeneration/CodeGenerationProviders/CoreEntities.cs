namespace CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreEntities : ExpressionFrameworkCSharpClassBase, ICodeGenerationProvider
{
    public override string Path => "ExpressionFramework.Core/DomainModel";
    public override string DefaultFileName => "Entities.template.generated.cs";

    public override object CreateModel()
        => GetImmutableClasses
        (
            GetCoreModels(),
            "ExpressionFramework.Core.DomainModel"
        )
        .Cast<IClass>()
        .Select
        (
            x => new ClassBuilder(x)
                .With(y =>
                {
                    if (y.Interfaces[0].StartsWith("ExpressionFramework.Abstractions.DomainModel.I", StringComparison.InvariantCulture)
                        && y.Interfaces[0].EndsWithAny(StringComparison.InvariantCulture, CustomBuilderTypes))
                    {
                        var className = GetClassName(y.Name);
                        y.Methods.Add(new ClassMethodBuilder()
                            .WithName("ToBuilder")
                            .WithTypeName($"ExpressionFramework.Abstractions.DomainModel.Builders.I{className}Builder")
                            .AddLiteralCodeStatements($"return new ExpressionFramework.Core.DomainModel.Builders.{y.Name}Builder(this);")
                        );
                    }
                })
                .Build()
        )
        .ToArray();

    private static string GetClassName(string className) // simplifies inherited types to base type, e.g. EmptyExpression -> Expression
        => CustomBuilderTypes.FirstOrDefault(x => className.EndsWith(x, StringComparison.InvariantCulture))
        ?? throw new NotSupportedException($"Unsupported type: [{className}]");
}
