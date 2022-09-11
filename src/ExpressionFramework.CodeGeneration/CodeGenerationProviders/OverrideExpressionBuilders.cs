﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideExpressionBuilders : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders/Expressions";
    public override string DefaultFileName => "Builders.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IExpression), "ExpressionFramework.Domain");
    protected override string BaseClassBuilderNameSpace => "ExpressionFramework.Domain.Tests.Support.Builders";

    public override object CreateModel()
        => GetImmutableBuilderClasses(
            GetOverrideExpressionModels(),
            "ExpressionFramework.Domain.Expressions",
            "ExpressionFramework.Domain.Tests.Support.Builders.Expressions");
}