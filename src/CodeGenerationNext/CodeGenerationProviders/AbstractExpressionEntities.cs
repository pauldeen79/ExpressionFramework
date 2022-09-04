namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractExpressionEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableClasses
        (
            GetAbstractExpressionModels(),
            "ExpressionFramework.Domain"
        )
        .Cast<IClass>()
        .Select
        (
            x => new ClassBuilder(x)
                .With(y =>
                {
                    //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
                    y.Methods.Add(new ClassMethodBuilder()
                        .WithName("ToBuilder")
                        .WithAbstract()
                        .WithTypeName($"{GetBuilderNamespace($"ExpressionFramework.Domain.{y.Name}")}.{y.Name}Builder")
                    );
                })
                .Build()
        )
        .ToArray();
}
