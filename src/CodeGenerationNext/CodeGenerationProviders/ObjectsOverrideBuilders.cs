namespace CodeGenerationNext.CodeGenerationProviders;

public class ObjectsOverrideBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Expressions/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), "ExpressionFramework.Domain");

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetOverrideModels(),
                                      "ExpressionFramework.Domain.Expressions",
                                      "ExpressionFramework.Domain.Expressions.Builders")
        .Cast<IClass>()
        .Select
        (
            //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
            x => new ClassBuilder(x)
                .AddMethods(new ClassMethodBuilder()
                    .WithName("BuildTyped")
                    .WithOverride()
                    .WithTypeName(x.GetFullName().Replace("ExpressionFramework.Domain.Expressions.Builders.", "ExpressionFramework.Domain.Expressions.", StringComparison.InvariantCulture).ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture))
                    .AddCodeStatements(x.Methods.Single(y => y.Name == "Build").CodeStatements.Select(z => z.CreateBuilder())))
                .With(y => y.Methods.Single(z => z.Name == "Build").CodeStatements.Clear())
                .With(y => y.Methods.Single(z => z.Name == "Build").AddLiteralCodeStatements("return BuildTyped();"))
                .With(y => y.Methods.Single(z => z.Name == "Build").WithTypeName($"ExpressionFramework.Domain.{GetEntityClassName(x.Name.ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture))}"))
                .WithBaseClass("ExpressionFramework.Domain.Builders." + x.BaseClass)
                .Build()
        )
        .ToArray();
}
