namespace CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override string SetMethodNameFormatString => string.Empty;
    protected override string FileNameSuffix => ".template.generated";
    protected override bool UseLazyInitialization => false; // this needs to be disabled, because extension method-based builders currently don't support this

    protected override string FormatInstanceTypeName(ITypeBase instance, bool forCreate)
    {
        if (instance.Namespace == "ExpressionFramework.Core.DomainModel")
        {
            return forCreate
                ? "ExpressionFramework.Core.DomainModel." + instance.Name
                : "ExpressionFramework.Abstractions.DomainModel.I" + instance.Name;
        }

        return string.Empty;
    }

    protected override void FixImmutableBuilderProperties(ClassBuilder classBuilder)
    {
        foreach (var property in classBuilder.Properties)
        {
            if (property.Name == "ValueDelegate")
            {
                //HACK: Fix nullable type in generic parameter
                property.TypeName = "System.Func`4[[System.Object?, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[ExpressionFramework.Abstractions.DomainModel.IExpression, ExpressionFramework.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[ExpressionFramework.Abstractions.IExpressionEvaluator, ExpressionFramework.Abstractions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null],[System.Object?, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]";
                // Fix initialization in builder c'tor, because the object it not nullable
                property.SetDefaultValueForBuilderClassConstructor(new Literal("new Func<object?, IExpression, IExpressionEvaluator, object?>((_, _, _) => null)"));
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
            else if (typeName.Contains("Collection<ExpressionFramework."))
            {
                if (typeName == "System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Abstractions.DomainModel.IExpression>")
                {
                    property.ConvertCollectionPropertyToBuilderOnBuilder
                    (
                        false,
                        typeof(ReadOnlyValueCollection<>).WithoutGenerics(),
                        "System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Abstractions.DomainModel.Builders.IExpressionBuilder>",
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
            }
        }
    }

    private static string GetCustomBuilderConstructorInitializeExpression(ClassPropertyBuilder property, string typeName)
    {
        if (typeName == "ExpressionFramework.Abstractions.DomainModel.IExpressionFunction"
            || typeName == "ExpressionFramework.Abstractions.DomainModel.IExpression"
            || typeName == "ExpressionFramework.Abstractions.DomainModel.ICompositeFunction")
        {
            return property.IsNullable
                ? "{0} = source.{0} == null ? null : source.{0}.ToBuilder()"
                : "{0} = source.{0}.ToBuilder()";
        }

        return property.IsNullable
            ? "{0} = source.{0} == null ? null : new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder" + "(source.{0})"
            : "{0} = new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder" + "(source.{0})";
    }

    private static Literal GetDefaultValueForBuilderClassConstructor(string typeName)
    {
        if (typeName == "ExpressionFramework.Abstractions.DomainModel.IExpression")
        {
            return new("new ExpressionFramework.Core.DomainModel.Builders.EmptyExpressionBuilder()");
        }

        if (typeName == "ExpressionFramework.Abstractions.DomainModel.ICompositeFunction")
        {
            return new("new ExpressionFramework.Core.DomainModel.Builders.EmptyCompositeFunctionBuilder()");
        }

        return new("new " + typeName.Replace("ExpressionFramework.Abstractions.DomainModel.I", "ExpressionFramework.Core.DomainModel.Builders.") + "Builder()");
    }
}
