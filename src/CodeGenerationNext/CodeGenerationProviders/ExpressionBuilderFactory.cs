namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionBuilderFactory : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain.Tests/Support/Builders";
    public override string DefaultFileName => "Builders.template.generated.cs";

    public override object CreateModel()
        //TODO: Move this functionality to base class in ModelFramework, i.e. GetBuilderFactoryClasses, that takes the abstract and override models and the entity and bulder namespaces
        => new[] { new ClassBuilder()
            .WithName("ExpressionBuilderFactory")
            .WithNamespace("ExpressionFramework.Domain.Tests.Support.Builders")
            .WithStatic()
            .AddMethods(new ClassMethodBuilder()
                .WithName("Create")
                .WithTypeName("ExpressionFramework.Domain.Tests.Support.Builders.ExpressionBuilder")
                .WithStatic()
                .AddParameter("expression", "ExpressionFramework.Domain.Expression")
                .AddLiteralCodeStatements("return expression switch {")
                .AddLiteralCodeStatements(GetOverrideExpressionModels().Select((x, i) => $"{x.Name} x{i}=> new ExpressionFramework.Domain.Tests.Support.Builders.Expressions.{x.Name}Builder(x{i}),"))
                .AddLiteralCodeStatements("ExpressionFramework.Domain.Tests.Support.UnknownExpression => new ExpressionFramework.Domain.Tests.Support.UnknownExpressionBuilder(),")
                .AddLiteralCodeStatements("_ => throw new ArgumentOutOfRangeException(\"Unknown expression type: \" + expression.GetType().FullName) };")
            )
            .Build() };
}
