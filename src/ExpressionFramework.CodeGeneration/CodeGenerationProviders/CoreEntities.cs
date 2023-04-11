﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class CoreEntities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.Domain;

    public override object CreateModel()
        => GetImmutableClasses(GetCoreModels(), Constants.Namespaces.Domain);
}
