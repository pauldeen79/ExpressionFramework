namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractOperatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableClasses
        (
            GetAbstractOperatorModels(),
            "ExpressionFramework.Domain"
        )
        .Cast<IClass>()
        .Select
        (
            //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
            x => new ClassBuilder(x)
                .AddMethods(new ClassMethodBuilder()
                    .WithName("ToBuilder")
                    .WithAbstract()
                    .WithTypeName($"{GetBuilderNamespace($"ExpressionFramework.Domain.{x.Name}")}.{x.Name}Builder")
                ).Build()
        )
        .ToArray();
}
