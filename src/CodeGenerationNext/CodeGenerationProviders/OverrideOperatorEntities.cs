namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Operators";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), "ExpressionFramework.Domain");
    protected override bool ValidateArgumentsInConstructor => false; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideOperatorModels(), "ExpressionFramework.Domain.Operators")
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
                        .AddLiteralCodeStatements($"return new ExpressionFramework.Domain.Operators.Builders.{y.Name}Builder(this);")
                    );
                })
                .Build()
        )
        .ToArray();
}
