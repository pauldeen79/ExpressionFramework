namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

public abstract class AdvancedCSharpClassBase : CSharpClassBase
{
    protected virtual string[] GetCustomBuilderTypes() => Array.Empty<string>();
    protected virtual string[] GetNonDomainTypes() => Array.Empty<string>();
    protected virtual Dictionary<string, string> GetCustomDefaultValueForBuilderClassConstructorValues() => new();
    protected virtual Dictionary<string, string> GetBuilderNamespaceMappings() => new();
    protected virtual Dictionary<string, string> GetModelMappings() => new();

    protected abstract string GetFullBasePath();
    protected abstract string RootNamespace { get; }

    protected virtual bool IsNotScaffolded(ITypeBase x, string classNameSuffix)
        => !File.Exists(System.IO.Path.Combine(GetFullBasePath(), Path, $"{x.Name}{classNameSuffix}.cs"));

    protected override void FixImmutableClassProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        => FixImmutableBuilderProperties(typeBaseBuilder);

    protected override void FixImmutableBuilderProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        foreach (var property in typeBaseBuilder.Properties)
        {
            var typeName = property.TypeName.FixTypeName();
            if (!property.IsValueType
                && typeName.StartsWithAny(StringComparison.InvariantCulture, GetBuilderNamespaceMappings().Keys.Select(x => $"{x}."))
                && !GetNonDomainTypes().Contains(property.TypeName))
            {
                property.ConvertSinglePropertyToBuilderOnBuilder
                (
                    $"{GetBuilderNamespace(typeName)}.{typeName.GetClassName()}Builder",
                    GetCustomBuilderConstructorInitializeExpressionForSingleProperty(property, typeName),
                    GetCustomBuilderMethodParameterExpression(typeName)
                );

                if (!property.IsNullable)
                {
                    property.SetDefaultValueForBuilderClassConstructor(GetDefaultValueForBuilderClassConstructor(typeName));
                }
            }
            else if (typeName.StartsWithAny(StringComparison.InvariantCulture, GetBuilderNamespaceMappings().Keys.Select(x => $"{RecordCollectionType.WithoutGenerics()}<{x}.")))
            {
                if (TypeNameNeedsSpecialTreatmentForBuilderInCollection(typeName))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        GetCustomCollectionArgumentType(typeName),
                        GetCustomBuilderConstructorInitializeExpressionForCollectionProperty(typeName)
                    );
                }
                else
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        GetCustomCollectionArgumentType(typeName)
                    );
                }
            }
            else if (typeName.IsBooleanTypeName() || typeName.IsNullableBooleanTypeName())
            {
                property.SetDefaultArgumentValueForWithMethod(true);
            }
        }
    }

    protected ITypeBase[] MapCodeGenerationModelsToDomain(IEnumerable<Type> types)
        => types
            .Select(x => x.ToClassBuilder(new ClassSettings())
                .WithNamespace(RootNamespace)
                .WithName(x.GetEntityClassName())
                .With(y => y.Properties.ForEach(z => GetModelMappings().ToList().ForEach(m => z.TypeName = z.TypeName.Replace(m.Key, m.Value))))
                .Build())
            .ToArray();

    protected string GetBuilderNamespace(string typeName)
        => GetBuilderNamespaceMappings()
            .Where(x => typeName.StartsWith(x.Key + ".", StringComparison.InvariantCulture))
            .Select(x => x.Value)
            .FirstOrDefault() ?? string.Empty;

    protected string ReplaceWithBuilderNamespaces(string typeName)
    {
        var match = GetBuilderNamespaceMappings()
            .Select(x => new { x.Key, x.Value })
            .FirstOrDefault(x => typeName.Contains($"{x.Key}.", StringComparison.InvariantCulture));

        return match == null
            ? typeName
            : typeName.Replace($"{match.Key}.", $"{match.Value}.", StringComparison.InvariantCulture);
    }

    /// <summary>
    /// Gets the base typename, based on a derived class.
    /// </summary>
    /// <param name="className">The typename to get the base classname from.</param>
    /// <returns>Base classname when found, otherwise string.Empty</returns>
    protected string GetEntityClassName(string className)
        => GetCustomBuilderTypes().FirstOrDefault(x => className.EndsWith(x, StringComparison.InvariantCulture)) ?? string.Empty;

    protected string GetEntityTypeName(string builderFullName)
    {
        var match = GetBuilderNamespaceMappings()
            .Select(x => new { x.Key, x.Value })
            .FirstOrDefault(x => builderFullName.StartsWith($"{x.Value}.", StringComparison.InvariantCulture));

        return match == null
            ? builderFullName.ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture)
            : builderFullName
                .Replace($"{match.Value}.", $"{match.Key}.", StringComparison.InvariantCulture)
                .ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture);
    }

    protected string? GetCustomBuilderMethodParameterExpression(string typeName)
        => string.IsNullOrEmpty(GetEntityClassName(typeName)) || GetCustomBuilderTypes().Contains(typeName.GetClassName())
            ? string.Empty
            : "{0}{2}.BuildTyped()";

    protected static object CreateServiceCollectionExtensions(
        string @namespace,
        string className,
        string methodName,
        ITypeBase[] types,
        Func<ITypeBase, string> formatDelegate)
        => new[] { new ClassBuilder()
            .WithNamespace(@namespace)
            .WithName(className)
            .WithStatic()
            .WithPartial()
            .AddMethods(new ClassMethodBuilder()
                .WithVisibility(Visibility.Private)
                .WithStatic()
                .WithName(methodName)
                .WithExtensionMethod()
                .WithType(typeof(IServiceCollection))
                .AddParameter("serviceCollection", typeof(IServiceCollection))
                .AddLiteralCodeStatements("return serviceCollection")
                .AddLiteralCodeStatements(types.Select(x => formatDelegate.Invoke(x)))
                .AddLiteralCodeStatements(";")
            )
            .Build() };

    protected static ITypeBase[] CreateBuilderFactoryModels(
        ITypeBase[] models,
        string classNamespace,
        string className,
        string classTypeName,
        string builderNamespace,
        string builderTypeName)
        => new[] { new ClassBuilder()
            .WithName(className)
            .WithNamespace(classNamespace)
            .WithStatic()
            .AddFields(new ClassFieldBuilder()
                .WithName("registeredTypes")
                .WithStatic()
                .WithTypeName($"Dictionary<Type,Func<{classTypeName},{builderTypeName}>>")
                .WithDefaultValue(GetBuilderFactoryModelDefaultValue(models, builderNamespace,classTypeName, builderTypeName))
            )
            .AddMethods(new ClassMethodBuilder()
                .WithName("Create")
                .WithTypeName($"{classNamespace}.{builderTypeName}")
                .WithStatic()
                .AddParameter("instance", classTypeName)
                .AddLiteralCodeStatements("return registeredTypes.ContainsKey(instance.GetType()) ? registeredTypes[instance.GetType()].Invoke(instance) : throw new ArgumentOutOfRangeException(\"Unknown instance type: \" + instance.GetType().FullName);"),
                new ClassMethodBuilder()
                .WithStatic()
                .WithName("Register")
                .AddParameter("type", typeof(Type))
                .AddParameter("createDelegate", $"Func<{classTypeName},{builderTypeName}>")
                .AddLiteralCodeStatements("registeredTypes.Add(type, createDelegate);")
            )
            .Build() };

    private static object GetBuilderFactoryModelDefaultValue(
        ITypeBase[] models,
        string builderNamespace,
        string classTypeName,
        string builderTypeName)
    {
        var builder = new StringBuilder();
        builder.AppendLine($"new Dictionary<Type, Func<{classTypeName}, {builderTypeName}>>")
               .AppendLine("{");
        foreach (var name in models.Select(x => x.Name))
        {
            builder.AppendLine("    {typeof(" + name + "),x => new " + builderNamespace + "." + name + "Builder((" + name + ")x)},");
        }
        builder.AppendLine("}");
        return new Literal(builder.ToString());
    }

    private bool TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(string typeName)
        => GetCustomBuilderTypes().Any(x => GetBuilderNamespaceMappings().Any(y => typeName == $"{y.Key}.{x}"));

    private string GetCustomCollectionArgumentType(string typeName)
        => ReplaceWithBuilderNamespaces(typeName).ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture);

    private bool TypeNameNeedsSpecialTreatmentForBuilderInCollection(string typeName)
        => GetCustomBuilderTypes().Any(x => GetBuilderNamespaceMappings().Any(y => typeName == $"System.Collections.Generic.IReadOnlyCollection<{y.Key}.{x}>"));

    private string GetCustomBuilderConstructorInitializeExpressionForSingleProperty(ClassPropertyBuilder property, string typeName)
    {
        if (TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(typeName))
        {
            return property.IsNullable
                ? "_{1}Delegate = new (() => source.{0} == null ? null : ExpressionFramework.Domain.Tests.Support.Builders." + GetEntityClassName(typeName) + "BuilderFactory.Create(source.{0}))"
                : "_{1}Delegate = new (() => ExpressionFramework.Domain.Tests.Support.Builders." + GetEntityClassName(typeName) + "BuilderFactory.Create(source.{0}))";
        }

        return property.IsNullable
            ? "_{1}Delegate = new (() => source.{0} == null ? null : new " + ReplaceWithBuilderNamespaces(typeName).GetNamespaceWithDefault() + ".{5}Builder(source.{0}))"
            : "_{1}Delegate = new (() => new " + ReplaceWithBuilderNamespaces(typeName).GetNamespaceWithDefault() + ".{5}Builder(source.{0}))";
    }

    private string GetCustomBuilderConstructorInitializeExpressionForCollectionProperty(string typeName)
        => "{0} = source.{0}.Select(x => ExpressionFramework.Domain.Tests.Support.Builders." + GetEntityClassName(typeName.GetGenericArguments()) + "BuilderFactory.Create(x)).ToList()";

    private Literal GetDefaultValueForBuilderClassConstructor(string typeName)
    {
        if (GetCustomDefaultValueForBuilderClassConstructorValues().ContainsKey(typeName))
        {
            return new(GetCustomDefaultValueForBuilderClassConstructorValues()[typeName]);
        }

        return new("new " + ReplaceWithBuilderNamespaces(typeName) + "Builder()");
    }
}
