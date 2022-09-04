namespace CodeGenerationNext.CodeGenerationProviders;

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
            x => new ClassBuilder(x)
                .With(y =>
                {
                    //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
                    var className = GetEntityClassName(y.Name);
                    y.Methods.Add(new ClassMethodBuilder()
                        .WithName("ToBuilder")
                        .WithOverride()
                        .WithTypeName($"ExpressionFramework.Domain.Builders.{className}Builder")
                        .AddLiteralCodeStatements($"return new ExpressionFramework.Domain.Expressions.Builders.{y.Name}Builder(this);")
                    );
                })
                .Build()
        )
        .ToArray();
}
