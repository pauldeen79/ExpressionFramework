namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

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
    protected override bool AddBackingFieldsForCollectionProperties => true;
    protected override bool AddPrivateSetters => true;

    protected override string GetFullBasePath()
        => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
            ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
            : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");

    protected override string[] GetCustomBuilderTypes()
        => GetCustomDefaultValueForBuilderClassConstructorValues().Select(x => x.Key.Split('.').Last()).ToArray();

    protected override Dictionary<string, string> GetCustomDefaultValueForBuilderClassConstructorValues()
        => new(GetAbstractModels().Select(x => new KeyValuePair<string, string>($"ExpressionFramework.Domain.{x.GetEntityClassName()}", "null")));

    protected override Dictionary<string, string> GetBuilderNamespaceMappings() => new(
        GetCustomBuilderTypes()
            .Select(x => new KeyValuePair<string, string>($"ExpressionFramework.Domain.{x}s", $"ExpressionFramework.Domain.Builders.{x}s"))
            .Concat(new[] { new KeyValuePair<string, string>("ExpressionFramework.Domain", "ExpressionFramework.Domain.Builders") }));

    protected override Dictionary<string, string> GetModelMappings() => new
    (
        new[] { new KeyValuePair<string, string>("ExpressionFramework.CodeGeneration.Models.I", "ExpressionFramework.Domain.") }
        .Concat(GetAbstractModels().Select(x => new KeyValuePair<string, string>($"ExpressionFramework.CodeGeneration.Models.{x.Name}s.I", $"ExpressionFramework.Domain.{x.Name}s.")))
        .Concat(new[]
        {
            new KeyValuePair<string, string>("ExpressionFramework.CodeGeneration.Models.Domains.", "ExpressionFramework.Domain.Domains."),
            new KeyValuePair<string, string>("ExpressionFramework.CodeGeneration.I", "ExpressionFramework.Domain.I")
        })
    );

    protected override string[] GetNonDomainTypes()
        => typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration")
            .Select(x => $"ExpressionFramework.Domain.{x.Name}")
            .ToArray();

    protected ITypeBase[] GetCoreModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == "ExpressionFramework.CodeGeneration.Models" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName())));

    protected ITypeBase[] GetAbstractModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.GetInterfaces().Length == 1)
                .Select(x => x.GetInterfaces()[0])
                .Distinct());

    protected ITypeBase[] GetOverrideModels(Type abstractType)
        // Note that you might think we could use x.GetInterfaces().FirstOrDefault() == abstractType, but that would give problems when inheriting like we do on ISingleEvaluatable for example
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == $"ExpressionFramework.CodeGeneration.Models.{abstractType.GetEntityClassName()}s"));
}
