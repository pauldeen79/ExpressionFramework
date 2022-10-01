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

    private static string CodeGenerationRootNamespace => "ExpressionFramework.CodeGeneration";

    protected override string GetFullBasePath()
        => Directory.GetCurrentDirectory().EndsWith("ExpressionFramework")
            ? System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"src/")
            : System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"../../../../");

    protected override string[] GetCustomBuilderTypes()
        => GetCustomDefaultValueForBuilderClassConstructorValues().Select(x => x.Key.Split('.').Last()).ToArray();

    protected override Dictionary<string, string> GetCustomDefaultValueForBuilderClassConstructorValues()
        => new(GetAbstractModels().Select(x => new KeyValuePair<string, string>($"{RootNamespace}.{x.GetEntityClassName()}", "null")));

    protected override Dictionary<string, string> GetBuilderNamespaceMappings()
        => new(GetCustomBuilderTypes()
            .Select(x => new KeyValuePair<string, string>($"{RootNamespace}.{x}s", $"{RootNamespace}.Builders.{x}s"))
            .Concat(new[] { new KeyValuePair<string, string>(RootNamespace, $"{RootNamespace}.Builders") }));

    protected override Dictionary<string, string> GetModelMappings() => new
    (
        new[] { new KeyValuePair<string, string>($"{CodeGenerationRootNamespace}.Models.I", $"{RootNamespace}.") }
        .Concat(GetAbstractModels().Select(x => new KeyValuePair<string, string>($"{CodeGenerationRootNamespace}.Models.{x.Name}s.I", $"{RootNamespace}.{x.Name}s.")))
        .Concat(new[]
        {
            new KeyValuePair<string, string>($"{CodeGenerationRootNamespace}.Models.Domains.", $"{RootNamespace}.Domains."),
            new KeyValuePair<string, string>($"{CodeGenerationRootNamespace}.I", $"{RootNamespace}.I")
        })
    );

    protected override string[] GetNonDomainTypes()
        => typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == CodeGenerationRootNamespace)
            .Select(x => $"{RootNamespace}.{x.Name}")
            .ToArray();

    protected ITypeBase[] GetCoreModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.Namespace == $"{CodeGenerationRootNamespace}.Models" && !GetCustomBuilderTypes().Contains(x.GetEntityClassName())));

    protected ITypeBase[] GetAbstractModels()
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.GetInterfaces().Length == 1)
                .Select(x => x.GetInterfaces()[0])
                .Distinct());

    protected ITypeBase[] GetOverrideModels(Type abstractType)
        => MapCodeGenerationModelsToDomain(
            typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
                .Where(x => x.IsInterface && x.GetInterfaces().FirstOrDefault() == abstractType));
}
