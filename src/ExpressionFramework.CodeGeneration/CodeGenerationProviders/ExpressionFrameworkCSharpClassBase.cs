namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : AdvancedCSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override string FileNameSuffix => ".template.generated";
    protected override string RootNamespace => "ExpressionFramework.Domain";

    protected override string[] GetCustomBuilderTypes() =>
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.GetInterfaces().Length == 1)
            .Select(x => x.GetInterfaces()[0].GetEntityClassName())
            .Distinct()
            .ToArray();

    protected override Dictionary<string, string> GetCustomDefaultValueForBuilderClassConstructorValues() => new()
    {
        { "ExpressionFramework.Domain.Expression", "new ExpressionFramework.Domain.Tests.Support.Builders.Expressions.EmptyExpressionBuilder()" },
        { "ExpressionFramework.Domain.Operator", "new ExpressionFramework.Domain.Tests.Support.Builders.Operators.EqualsOperatorBuilder()" },
    };

    protected override Dictionary<string, string> GetBuilderNamespaceMappings() => new(
        GetCustomBuilderTypes()
            .Select(x => new KeyValuePair<string, string>($"ExpressionFramework.Domain.{x}s", $"ExpressionFramework.Domain.Tests.Support.Builders.{x}s"))
            .Concat(new[] { new KeyValuePair<string, string>("ExpressionFramework.Domain", "ExpressionFramework.Domain.Tests.Support.Builders"), }));

    protected override Dictionary<string, string> GetModelMappings() => new()
    {
        { "ExpressionFramework.CodeGeneration.Models.I", "ExpressionFramework.Domain." },
        { "ExpressionFramework.CodeGeneration.Models.Expressions.I", "ExpressionFramework.Domain.Expressions." },
        { "ExpressionFramework.CodeGeneration.Models.Operators.I", "ExpressionFramework.Domain.Operators." },
        { "ExpressionFramework.CodeGeneration.Models.Requests.I", "ExpressionFramework.Domain.Requests." },
        { "ExpressionFramework.CodeGeneration.Models.Domains.", "ExpressionFramework.Domain.Domains." },
        { "ExpressionFramework.CodeGeneration.I", "ExpressionFramework.Domain.I" },
    };

    protected override string[] GetNonDomainTypes() =>
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration")
            .Select(x => $"ExpressionFramework.Domain.{x.Name}")
            .ToArray();

    protected ITypeBase[] GetCoreModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName())));

    protected ITypeBase[] GetRequestModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Requests"));

    protected ITypeBase[] GetAbstractExpressionModels() => MapCodeGenerationModelsToDomain(new[]
    {
        typeof(IExpression),
    });

    protected ITypeBase[] GetAbstractOperatorModels() => MapCodeGenerationModelsToDomain(new[]
    {
        typeof(IOperator),
    });

    protected ITypeBase[] GetOverrideExpressionModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Expressions"));

    protected ITypeBase[] GetOverrideOperatorModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models.Operators"));

    protected override string GetFullBasePath() => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
        ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
        : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");
}
