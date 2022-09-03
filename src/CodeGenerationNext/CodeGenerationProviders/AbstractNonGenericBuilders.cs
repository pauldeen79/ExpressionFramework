namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractNonGenericBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override string FileNameSuffix => ".nongeneric.template.generated";

    public override object CreateModel()
        => GetImmutableNonGenericBuilderClasses(GetAbstractModels(),
                                                "ExpressionFramework.Domain",
                                                "ExpressionFramework.Domain.Builders")
        .Cast<IClass>()
        .Select
        (
            //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
            x => new ClassBuilder(x)
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Build")
                    .WithAbstract()
                    .WithTypeName(x.GetFullName().Replace("ExpressionFramework.Domain.Builders.", "ExpressionFramework.Domain.", StringComparison.InvariantCulture).ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture)))
                .Build()
        )
        .ToArray();
}
