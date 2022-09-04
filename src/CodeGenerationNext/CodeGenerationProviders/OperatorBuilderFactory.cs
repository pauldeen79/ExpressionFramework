﻿namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        //TODO: Move this functionality to base class in ModelFramework, i.e. GetBuilderFactoryClasses, that takes the abstract and override models and the entity and bulder namespaces
        => new[] { new ClassBuilder()
            .WithName("OperatorBuilderFactory")
            .WithNamespace("ExpressionFramework.Domain.Tests.Support.Builders")
            .WithStatic()
            .AddMethods(new ClassMethodBuilder()
                .WithName("Create")
                .WithTypeName("ExpressionFramework.Domain.Tests.Support.Builders.OperatorBuilder")
                .WithStatic()
                .AddParameter("operator", "ExpressionFramework.Domain.Operator")
                .AddLiteralCodeStatements("return @operator switch {")
                .AddLiteralCodeStatements(GetOverrideOperatorModels().Select((x, i) => $"{x.Name} x{i}=> new ExpressionFramework.Domain.Tests.Support.Builders.Operators.{x.Name}Builder(x{i}),"))
                .AddLiteralCodeStatements("_ => throw new ArgumentOutOfRangeException(\"Unknown operator type: \" + @operator.GetType().FullName) };")
            )
            .Build() };
}
