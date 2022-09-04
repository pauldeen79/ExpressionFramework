﻿namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class AbstractExpressionBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;

    public override object CreateModel()
        => GetImmutableBuilderClasses(GetAbstractExpressionModels(),
                                      "ExpressionFramework.Domain",
                                      "ExpressionFramework.Domain.Tests.Support.Builders");
}
