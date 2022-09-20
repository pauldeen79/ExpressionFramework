﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override string FileNameSuffix => ".template.generated";
    protected override string RootNamespace => "ExpressionFramework.Domain";
    protected override Type BuilderClassCollectionType => typeof(IEnumerable<>);

    protected override string GetFullBasePath()
        => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
            ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
            : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");

    protected override string[] GetCustomBuilderTypes()
        => typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.GetInterfaces().Length == 1)
            .Select(x => x.GetInterfaces()[0].GetEntityClassName())
            .Distinct()
            .ToArray();

    protected override Dictionary<string, string> GetCustomDefaultValueForBuilderClassConstructorValues() => new()
    {
        { "ExpressionFramework.Domain.Expression", "new ExpressionFramework.Domain.Builders.Expressions.EmptyExpressionBuilder()" },
        { "ExpressionFramework.Domain.Operator", "new ExpressionFramework.Domain.Builders.Operators.EqualsOperatorBuilder()" },
        { "ExpressionFramework.Domain.Evaluatable", "new ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder()" },
    };

    protected override Dictionary<string, string> GetBuilderNamespaceMappings() => new(
        GetCustomBuilderTypes()
            .Select(x => new KeyValuePair<string, string>($"ExpressionFramework.Domain.{x}s", $"ExpressionFramework.Domain.Builders.{x}s"))
            .Concat(new[] { new KeyValuePair<string, string>("ExpressionFramework.Domain", "ExpressionFramework.Domain.Builders"), }));

    protected override Dictionary<string, string> GetModelMappings() => new()
    {
        { "ExpressionFramework.CodeGeneration.Models.I", "ExpressionFramework.Domain." },
        { "ExpressionFramework.CodeGeneration.Models.Expressions.I", "ExpressionFramework.Domain.Expressions." },
        { "ExpressionFramework.CodeGeneration.Models.Operators.I", "ExpressionFramework.Domain.Operators." },
        { "ExpressionFramework.CodeGeneration.Models.Evaluatables.I", "ExpressionFramework.Domain.Evaluatables." },
        { "ExpressionFramework.CodeGeneration.Models.Domains.", "ExpressionFramework.Domain.Domains." },
        { "ExpressionFramework.CodeGeneration.I", "ExpressionFramework.Domain.I" },
    };

    protected override string[] GetNonDomainTypes()
        => typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration")
            .Select(x => $"ExpressionFramework.Domain.{x.Name}")
            .ToArray();

    protected ITypeBase[] GetCoreModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName())));

    protected ITypeBase[] GetAbstractExpressionModels()
        => MapCodeGenerationModelsToDomain(new[]
        {
            typeof(IExpression),
        });

    protected ITypeBase[] GetAbstractOperatorModels()
        => MapCodeGenerationModelsToDomain(new[]
        {
            typeof(IOperator),
        });

    protected ITypeBase[] GetAbstractEvaluatableModels()
        => MapCodeGenerationModelsToDomain(new[]
        {
            typeof(IEvaluatable),
        });

    protected ITypeBase[] GetOverrideExpressionModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Expressions"));

    protected ITypeBase[] GetOverrideOperatorModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Operators"));

    protected ITypeBase[] GetOverrideEvaluatableModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Evaluatables"));
}
