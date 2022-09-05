namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override string FileNameSuffix => ".template.generated";

    protected static readonly string[] CustomBuilderTypes = new[]
    {
        "Expression",
        "Operator",
    };

    protected static readonly Dictionary<string, string> CustomDefaultValueForBuilderClassConstructorValues = new()
    {
        { "ExpressionFramework.Domain.Expression", "new ExpressionFramework.Domain.Tests.Support.Builders.Expressions.EmptyExpressionBuilder()" },
        { "ExpressionFramework.Domain.Operator", "new ExpressionFramework.Domain.Tests.Support.Builders.Operators.EqualsOperatorBuilder()" },
    };

    protected static readonly Dictionary<string, string> BuilderNamespaceMappings = new()
    {
        { "ExpressionFramework.Domain.Expressions", "ExpressionFramework.Domain.Tests.Support.Builders.Expressions" },
        { "ExpressionFramework.Domain.Operators", "ExpressionFramework.Domain.Tests.Support.Builders.Operators" },
        { "ExpressionFramework.Domain", "ExpressionFramework.Domain.Tests.Support.Builders" },
    };

    protected static readonly Dictionary<string, string> ModelMappings = new()
    {
        { "CodeGenerationNext.Models.I", "ExpressionFramework.Domain." },
        { "CodeGenerationNext.Models.Expressions.I", "ExpressionFramework.Domain.Expressions." },
        { "CodeGenerationNext.Models.Operators.I", "ExpressionFramework.Domain.Operators." },
        { "CodeGenerationNext.Models.Requests.", "ExpressionFramework.Domain.Requests." },
        { "CodeGenerationNext.Models.", "ExpressionFramework.Domain.Domains." },
    };

    protected override void FixImmutableClassProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        => FixImmutableBuilderProperties(typeBaseBuilder);

    protected override void FixImmutableBuilderProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        foreach (var property in typeBaseBuilder.Properties)
        {
            var typeName = property.TypeName.FixTypeName();
            if (!property.IsValueType && typeName.StartsWithAny(StringComparison.InvariantCulture, BuilderNamespaceMappings.Keys.Select(x => $"{x}.")))
            {
                property.ConvertSinglePropertyToBuilderOnBuilder
                (
                    GetBuilderTypeName(typeName),
                    GetCustomBuilderConstructorInitializeExpression(property, typeName),
                    //TODO: See if we can move this to ModelFramework... But how do we know that we need the casting? Or can we call BuildTyped?
                    string.IsNullOrEmpty(GetEntityClassName(typeName))
                        ? string.Empty
                        : "({3}){0}{2}.Build()"
                );

                property.SetDefaultValueForBuilderClassConstructor(GetDefaultValueForBuilderClassConstructor(typeName));
            }
            else if (typeName.StartsWithAny(StringComparison.InvariantCulture, BuilderNamespaceMappings.Keys.Select(x => $"{RecordCollectionType.WithoutGenerics()}<{x}.")))
            {
                if (TypeNameNeedsSpecialTreatmentForBuilderInCollection(typeName))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        ReplaceWithBuilderNamespaces(typeName)
                            .ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                        "{0} = source.{0}.Select(x => ExpressionFramework.Domain.Tests.Support.Builders." + GetEntityClassName(typeName.GetGenericArguments()) + "BuilderFactory.Create(x)).ToList()"
                    );
                }
                else
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        ReplaceWithBuilderNamespaces(typeName)
                            .ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture)
                    );
                }
            }
            else if (typeName.IsBooleanTypeName() || typeName.IsNullableBooleanTypeName())
            {
                property.SetDefaultArgumentValueForWithMethod(true);
            }
            else if (CustomDefaultValueForBuilderClassConstructorValues.ContainsKey(typeName))
            {
                // Allow default values for other types as well (not just domain model ones)
                property.SetDefaultValueForBuilderClassConstructor(GetDefaultValueForBuilderClassConstructor(typeName));
            }
        }
    }

    protected static ITypeBase[] GetCoreModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace.In("CodeGenerationNext.Models", "CodeGenerationNext.Requests") && !CustomBuilderTypes.Contains(x.GetEntityClassName()))
            .ToArray());

    protected static ITypeBase[] GetAbstractExpressionModels() => MapCodeGenerationModelsToDomain(new[]
    {
        typeof(IExpression),
    });

    protected static ITypeBase[] GetAbstractOperatorModels() => MapCodeGenerationModelsToDomain(new[]
    {
        typeof(IOperator),
    });

    protected static ITypeBase[] GetOverrideExpressionModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "CodeGenerationNext.Models.Expressions")
            .ToArray());

    protected static ITypeBase[] GetOverrideOperatorModels() => MapCodeGenerationModelsToDomain(
        typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes()
            .Where(x => x.IsInterface && x.Namespace == "CodeGenerationNext.Models.Operators")
            .ToArray());

    protected static string GetBuilderNamespace(string typeName)
        => BuilderNamespaceMappings
            .Where(x => typeName.StartsWith(x.Key + ".", StringComparison.InvariantCulture))
            .Select(x => x.Value)
            .FirstOrDefault() ?? string.Empty;

    protected static string GetBuilderTypeName(string typeName)
        => $"{GetBuilderNamespace(typeName)}.{typeName.GetClassName()}Builder";

    protected static string ReplaceWithBuilderNamespaces(string typeName)
    {
        var match = BuilderNamespaceMappings
            .Select(x => new { x.Key, x.Value })
            .FirstOrDefault(x => typeName.Contains($"{x.Key}.", StringComparison.InvariantCulture));

        return match == null
            ? typeName
            : typeName.Replace($"{match.Key}.", $"{match.Value}.", StringComparison.InvariantCulture);
    }

    // simplifies inherited types to base type, e.g. EmptyExpression -> Expression
    protected static string GetEntityClassName(string className)
        => CustomBuilderTypes.FirstOrDefault(x => className.EndsWith(x, StringComparison.InvariantCulture)) ?? string.Empty;

    protected static string GetEntityTypeName(string builderFullName)
    {
        var match = BuilderNamespaceMappings
            .Select(x => new { x.Key, x.Value })
            .FirstOrDefault(x => builderFullName.StartsWith($"{x.Value}.", StringComparison.InvariantCulture));

        return match == null
            ? builderFullName.ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture)
            : builderFullName
                .Replace($"{match.Value}.", $"{match.Key}.", StringComparison.InvariantCulture)
                .ReplaceSuffix("Builder", string.Empty, StringComparison.InvariantCulture);
    }

    //TODO: Move to ModelFramework, like namespace mappings for models contained in code generation projects
    private static ITypeBase[] MapCodeGenerationModelsToDomain(Type[] types)
        => types
            .Select(x => x.ToClassBuilder(new ClassSettings())
                .WithNamespace("ExpressionFramework.Domain")
                .WithName(x.Name.Substring(1))
                .With(y => y.Properties.ForEach(z => ModelMappings.ToList().ForEach(m => z.TypeName = z.TypeName.Replace(m.Key, m.Value))))
                .Build())
            .ToArray();

    private static bool TypeNameNeedsSpecialTreatmentForBuilderInCollection(string typeName)
        => CustomBuilderTypes.Any(x => BuilderNamespaceMappings.Any(y => typeName == $"System.Collections.Generic.IReadOnlyCollection<{y.Key}.{x}>"));

    private static string GetCustomBuilderConstructorInitializeExpression(ClassPropertyBuilder property, string typeName)
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

    private static bool TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(string typeName)
        => CustomBuilderTypes.Any(x => BuilderNamespaceMappings.Any(y => typeName == $"{y.Key}.{x}"));

    private static Literal GetDefaultValueForBuilderClassConstructor(string typeName)
    {
        if (CustomDefaultValueForBuilderClassConstructorValues.ContainsKey(typeName))
        {
            return new(CustomDefaultValueForBuilderClassConstructorValues[typeName]);
        }

        return new("new " + ReplaceWithBuilderNamespaces(typeName) + "Builder()");
    }
}
