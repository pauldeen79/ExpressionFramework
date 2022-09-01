namespace CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override string SetMethodNameFormatString => string.Empty;
    protected override string AddMethodNameFormatString => string.Empty;
    protected override string FileNameSuffix => ".template.generated";
    protected override bool UseLazyInitialization => false; // this needs to be disabled, because extension method-based builders currently don't support this

    protected static readonly string[] CustomBuilderTypes = new[]
    {
        "Expression",
        "ExpressionFunction",
        "AggregateFunction",
        "Case",
        "Condition",
    };

    protected static readonly Dictionary<string, string> CustomDefaultValueForBuilderClassConstructorValues = new()
    {
        { "ExpressionFramework.Abstractions.DomainModel.IExpression", "new ExpressionFramework.Core.DomainModel.Builders.EmptyExpressionBuilder()" },
        { "ExpressionFramework.Abstractions.DomainModel.IAggregateFunction", "new ExpressionFramework.Core.AggregateFunctions.EmptyAggregateFunctionBuilder()" },
    };

    protected override string FormatInstanceTypeName(ITypeBase instance, bool forCreate)
    {
        if (forCreate)
        {
            // For creation, the typename doesn't have to be altered/formatted.
            return string.Empty;
        }

        if (instance.Namespace == "ExpressionFramework.Core.DomainModel")
        {
            return "ExpressionFramework.Abstractions.DomainModel.I" + instance.Name;
        }

        return string.Empty;
    }

    protected override void FixImmutableClassProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
        => FixImmutableBuilderProperties(typeBaseBuilder);

    protected override void FixImmutableBuilderProperties<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        foreach (var property in typeBaseBuilder.Properties)
        {
            if (property.Name == "ValueDelegate")
            {
                //HACK: Fix nullable type in generic parameter
                property.TypeName = "System.Func`5[[System.Object?, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[ExpressionFramework.Abstractions.DomainModel.IExpression, ExpressionFramework.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[ExpressionFramework.Abstractions.IExpressionEvaluator, ExpressionFramework.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Object?, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]";
                // Fix initialization in builder c'tor, because the object is not nullable
                property.SetDefaultValueForBuilderClassConstructor(new Literal("new((_, _, _) => null)"));
            }

            var typeName = property.TypeName.FixTypeName();
            if (typeName.StartsWith("ExpressionFramework.Abstractions.DomainModel.I", StringComparison.InvariantCulture))
            {
                property.ConvertSinglePropertyToBuilderOnBuilder
                (
                    typeName.Replace("ExpressionFramework.Abstractions.DomainModel.", "ExpressionFramework.Abstractions.DomainModel.Builders.") + "Builder",
                    GetCustomBuilderConstructorInitializeExpression(property, typeName)
                );

                property.SetDefaultValueForBuilderClassConstructor(GetDefaultValueForBuilderClassConstructor(typeName));
            }
            else if (typeName.StartsWith($"{RecordCollectionType.WithoutGenerics()}<ExpressionFramework.Abstractions.DomainModel.I", StringComparison.InvariantCulture))
            {
                if (TypeNameNeedsSpecialTreatmentForBuilderInCollection(typeName))
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        typeName
                            .Replace("DomainModel.", "DomainModel.Builders.", StringComparison.InvariantCulture)
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
                        typeName
                            .Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.", StringComparison.InvariantCulture)
                            .ReplaceSuffix(">", "Builder>", StringComparison.InvariantCulture)
                    );
                }
            }
            else if (typeName.IsBooleanTypeName() || typeName.IsNullableBooleanTypeName())
            {
                property.SetDefaultArgumentValueForWithMethod(true);
                if (property.Name == "Continue")
                {
                    property.SetDefaultValueForBuilderClassConstructor(new Literal("true"));
                }
            }
        }
    }

    private static bool TypeNameNeedsSpecialTreatmentForBuilderInCollection(string typeName)
        => CustomBuilderTypes.Any(x => typeName == $"System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Abstractions.DomainModel.I{x}>");

    private static string GetCustomBuilderConstructorInitializeExpression(ClassPropertyBuilder property, string typeName)
    {
        if (TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(typeName))
        {
            return property.IsNullable
                ? "{0} = source.{0} == null ? null : source.{0}.ToBuilder()"
                : "{0} = source.{0}.ToBuilder()";
        }

        return property.IsNullable
            ? "{0} = source.{0} == null ? null : new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder" + "(source.{0})"
            : "{0} = new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder" + "(source.{0})";
    }

    private static bool TypeNameNeedsSpecialTreatmentForBuilderConstructorInitializeExpression(string typeName)
        => CustomBuilderTypes.Any(x => typeName == $"ExpressionFramework.Abstractions.DomainModel.I{x}");

    private static Literal GetDefaultValueForBuilderClassConstructor(string typeName)
    {
        if (CustomDefaultValueForBuilderClassConstructorValues.ContainsKey(typeName))
        {
            return new(CustomDefaultValueForBuilderClassConstructorValues[typeName]);
        }

        return new("new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder()");
    }
}
