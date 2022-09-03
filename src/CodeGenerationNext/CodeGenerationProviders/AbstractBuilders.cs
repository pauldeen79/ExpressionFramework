﻿namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetAbstractModels(),
                                      "ExpressionFramework.Domain",
                                      "ExpressionFramework.Domain.Builders")
        .Cast<IClass>()
        .Select
        (
            //TODO: Move to ModelFramework (configurable if we want typed or untyped Build method, maybe even BuildTyped?)
            x => new ClassBuilder(x)
                .Chain(y => y.Methods.Single(z => z.Name == "Build").Name = "BuildTyped")
                .Build()
        )
        .ToArray();
}
