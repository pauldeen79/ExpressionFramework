﻿namespace CodeGenerationNext.CodeGenerationProviders;

public class AbstractRecords : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain";
    public override string DefaultFileName => "Entities.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableClasses
        (
            GetAbstractModels(),
            "ExpressionFramework.Domain"
        )
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
                        .WithAbstract()
                        .WithTypeName($"ExpressionFramework.Domain.Builders.{className}Builder")
                    );
                })
                .Build()
        )
        .ToArray();
}
