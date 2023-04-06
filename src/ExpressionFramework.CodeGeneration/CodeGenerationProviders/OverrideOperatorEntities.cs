﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OverrideOperatorEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Operators";
    public override string DefaultFileName => "Entities.generated.cs";

    protected override bool EnableEntityInheritance => true;
    protected override bool EnableBuilderInhericance => true;
    protected override IClass? BaseClass => CreateBaseclass(typeof(IOperator), "ExpressionFramework.Domain");
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Never; // there are no properties in operators, so this is not necessary

    public override object CreateModel()
        => GetImmutableClasses(GetOverrideModels(typeof(IOperator)), "ExpressionFramework.Domain.Operators");
}
