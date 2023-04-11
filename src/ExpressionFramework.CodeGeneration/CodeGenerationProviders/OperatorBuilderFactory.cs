﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Namespaces.DomainBuilders;

    public override object CreateModel()
        => CreateBuilderFactoryModels(
            GetOverrideModels(typeof(IOperator)),
            new(Constants.Namespaces.DomainBuilders,
            nameof(OperatorBuilderFactory),
            $"{Constants.Namespaces.Domain}.Operator",
            $"{Constants.Namespaces.DomainBuilders}.Operators",
            "OperatorBuilder",
            $"{Constants.Namespaces.Domain}.Operators"));
}
