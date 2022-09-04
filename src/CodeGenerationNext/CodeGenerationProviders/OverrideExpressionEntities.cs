namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideExpressionEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Expressions";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), "ExpressionFramework.Domain");

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideExpressionModels(), "ExpressionFramework.Domain.Expressions")
        .Cast<IClass>()
        .Select
        (
            //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
            x => new ClassBuilder(x)
                .AddMethods(new ClassMethodBuilder()
                    .WithName("ToBuilder")
                    .WithOverride()
                    .WithTypeName($"ExpressionFramework.Domain.Builders.{GetEntityClassName(x.Name)}Builder")
                    .AddLiteralCodeStatements($"return new ExpressionFramework.Domain.Expressions.Builders.{x.Name}Builder(this);")
                )
                .Build()
        )
        .ToArray();
}
