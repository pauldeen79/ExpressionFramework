using System.Linq;

namespace CodeGenerationNext.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override string FileNameSuffix => ".template.generated";
    protected override bool AddNullChecks => true;

    protected static readonly string[] CustomBuilderTypes = new[]
    {
        "Expression",
    };

    protected static readonly Dictionary<string, string> CustomDefaultValueForBuilderClassConstructorValues = new()
    {
        { "ExpressionFramework.Domain.Expression", "new ExpressionFramework.Domain.Expressions.Builders.EmptyExpressionBuilder()" },
    };

    protected static readonly Dictionary<string, string> NamespaceMappings = new()
    {
        { "ExpressionFramework.Domain.Expressions", "ExpressionFramework.Domain.Expressions.Builders" },
        { "ExpressionFramework.Domain", "ExpressionFramework.Domain.Builders" },
    };

    protected static string GetBuilderNamespace(string typeName)
        => NamespaceMappings
            .Where(x => typeName.StartsWith(x.Key + ".", StringComparison.InvariantCulture))
            .Select(x => x.Value)
            .FirstOrDefault() ?? string.Empty;

    protected static string GetBuilderTypeName(string typeName)
        => $"{GetBuilderNamespace(typeName)}.{typeName.GetClassName()}Builder";

    protected static string ReplaceWithBuilderNamespaces(string typeName)
    {
        foreach (var mapping in NamespaceMappings)
        {
            if (typeName.Contains($"{mapping.Key}.", StringComparison.InvariantCulture))
            {
                typeName = typeName.Replace($"{mapping.Key}.", $"{mapping.Value}.", StringComparison.InvariantCulture);
                break;
            }
        }
        return typeName;
    }
    
    protected override void FixImmutableClassProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        => FixImmutableBuilderProperties(typeBaseBuilder);

    protected override void FixImmutableBuilderProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        foreach (var property in typeBaseBuilder.Properties)
        {
            var typeName = property.TypeName.FixTypeName();
            if (!property.IsValueType && typeName.StartsWithAny(StringComparison.InvariantCulture, NamespaceMappings.Keys.Select(x => $"{x}.")))
            {
                property.ConvertSinglePropertyToBuilderOnBuilder
                (
                    GetBuilderTypeName(typeName),
                    GetCustomBuilderConstructorInitializeExpression(property, typeName),
                    //TODO: See if we can move this to ModelFramework... But how do we know that we need the casting? Or can we call BuildTyped?
                    string.IsNullOrEmpty(GetEntityClassName(typeName))
                        ? string.Empty
                        : "({3}){0}{2}.Build()" //"(" + typeName +"){0}{2}.Build()"
                );

                property.SetDefaultValueForBuilderClassConstructor(GetDefaultValueForBuilderClassConstructor(typeName));
            }
            else if (typeName.StartsWithAny(StringComparison.InvariantCulture, NamespaceMappings.Keys.Select(x => $"{RecordCollectionType.WithoutGenerics()}<{x}.")))
            {
                if (TypeNameNeedsSpecialTreatmentForBuilderInCollection(typeName))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        ReplaceWithBuilderNamespaces(typeName)
                            .ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture),
                        "{0} = source.{0}.Select(x => x.ToBuilder()).ToList()"
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

    protected static ITypeBase[] GetCoreModels() => MapToDomain(new[]
    {
        typeof(ICondition),
        typeof(IAggregateFunctionResultValue),
        typeof(ICase),
    });

    protected static ITypeBase[] GetAbstractModels() => MapToDomain(new[]
    {
        typeof(IExpression),
    });

    protected static ITypeBase[] GetOverrideModels() => MapToDomain(new[]
    {
        typeof(IAccumulatorExpression),
        typeof(IAggregateExpression),
        typeof(IConditionalExpression),
        typeof(IConstantExpression),
        typeof(IContextExpression),
        typeof(IChainedExpression),
        typeof(IEmptyExpression),
        typeof(IFieldExpression),
        typeof(ISwitchExpression),
    });

    protected static string GetEntityClassName(string className) // simplifies inherited types to base type, e.g. EmptyExpression -> Expression
        => CustomBuilderTypes.FirstOrDefault(x => className.EndsWith(x, StringComparison.InvariantCulture)) ?? string.Empty;

    //TODO: Move to ModelFramework, like namespace mappings for models contained in code generation projects
    private static ITypeBase[] MapToDomain(Type[] types)
        => types
            .Select(x => x.ToClassBuilder(new ClassSettings())
                .WithNamespace("ExpressionFramework.Domain")
                .WithName(x.Name.Substring(1))
                .With(y => y.Properties.ForEach(z => z.TypeName = z.TypeName
                    .Replace("CodeGenerationNext.Models.I", "ExpressionFramework.Domain.")
                    .Replace("CodeGenerationNext.Models.Expressions.I", "ExpressionFramework.Domain.Expressions.")
                    .Replace("CodeGenerationNext.Models.", "ExpressionFramework.Domain.Domains.")))
                .Build())
            .ToArray();

    private static bool TypeNameNeedsSpecialTreatmentForBuilderInCollection(string typeName)
        //=> CustomBuilderTypes.Any(x => typeName == $"System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.{x}>");
        => CustomBuilderTypes.Any(x => NamespaceMappings.Any(y => typeName == $"System.Collections.Generic.IReadOnlyCollection<{y.Key}.{x}>"));

    private static string GetCustomBuilderConstructorInitializeExpression(ClassPropertyBuilder property, string typeName)
    {
        if (TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(typeName))
        {
            //return property.IsNullable
            //    ? "{0} = source.{0} == null ? null : source.{0}.ToBuilder()"
            //    : "{0} = source.{0}.ToBuilder()";

            return property.IsNullable
                ? "_{1}Delegate = new (() => source.{0} == null ? null : source.{0}.ToBuilder())"
                : "_{1}Delegate = new (() => source.{0}.ToBuilder())";
        }

        //return property.IsNullable
        //    ? "{0} = source.{0} == null ? null : new " + typeName.Replace("ExpressionFramework.Domain.", "ExpressionFramework.Domain.Builders.", StringComparison.InvariantCulture) + "Builder" + "(source.{0})"
        //    : "{0} = new " + typeName.Replace("ExpressionFramework.Domain.", "ExpressionFramework.Domain.Builders.", StringComparison.InvariantCulture) + "Builder" + "(source.{0})";

        return property.IsNullable
            ? "_{1}Delegate = new (() => source.{0} == null ? null : new " + ReplaceWithBuilderNamespaces(typeName).GetNamespaceWithDefault() + ".{5}Builder(source.{0}))"
            : "_{1}Delegate = new (() => new " + ReplaceWithBuilderNamespaces(typeName).GetNamespaceWithDefault() + ".{5}Builder(source.{0}))";
    }

    private static bool TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(string typeName)
        //=> CustomBuilderTypes.Any(x => typeName == $"ExpressionFramework.Domain.{x}");
        => CustomBuilderTypes.Any(x => NamespaceMappings.Any(y => typeName == $"{y.Key}.{x}"));

    private static Literal GetDefaultValueForBuilderClassConstructor(string typeName)
    {
        if (CustomDefaultValueForBuilderClassConstructorValues.ContainsKey(typeName))
        {
            return new(CustomDefaultValueForBuilderClassConstructorValues[typeName]);
        }

        return new("new " + ReplaceWithBuilderNamespaces(typeName) + "Builder()");
    }
}
