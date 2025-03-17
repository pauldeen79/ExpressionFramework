namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class ExpressionFrameworkCSharpClassBase(IPipelineService pipelineService) : CsharpClassGeneratorPipelineCodeGenerationProviderBase(pipelineService)
{
    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string LastGeneratedFilesFilename => string.Empty;
    public override Encoding Encoding => Encoding.UTF8;

    protected override Type EntityCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type EntityConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override Type BuilderCollectionType => typeof(ObservableCollection<>);

    protected override string ProjectName => "ExpressionFramework";
    protected override string CoreNamespace => "ExpressionFramework.Core";
    protected override bool CopyAttributes => true;
    protected override bool CopyInterfaces => true;
    protected override bool CreateRecord => true;
    protected override bool GenerateMultipleFiles => false;
    protected override bool EnableGlobalUsings => true;

    protected static IEnumerable<AttributeBuilder> GetFunctionAttributes(TypeBase typeBase)
    {
        var propertiesRequired = typeBase.Properties.Select(GetRequired).ToArray();

        return typeBase.Properties.Select((x, counter) =>
        {
            var isNullable = !propertiesRequired[counter];
            if (!isNullable && counter > 0 && propertiesRequired.Take(counter).Any(x => !x))
            {
                isNullable = true;
            }

            return new AttributeBuilder()
                .WithName(typeof(FunctionArgumentAttribute))
                .AddParameters(
                    new AttributeParameterBuilder().WithValue(x.Name),
                    new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({FixTypeName(x.TypeName, false)})")),
                    new AttributeParameterBuilder().WithValue(!isNullable)
                );
        });
    }

    protected static string GetFunctionArgumentsAddString(TypeBase typeBase)
    {
        var builder = new StringBuilder();
        var counter = 0;
        var propertiesRequired = typeBase.Properties.Select(GetRequired).ToArray();

        foreach (var prop in typeBase.Properties)
        {
            var defaultValue = string.Empty;
            var isNullable = !propertiesRequired[counter];
            if (!isNullable && counter > 0 && propertiesRequired.Take(counter).Any(x => !x))
            {
                isNullable = true;
            }

            if (isNullable)
            {
                defaultValue = $", default({FixTypeName(prop.TypeName, false)})";
                isNullable = false;
            }

            builder.Append(@$".Add(""{prop.Name}"", () => context.GetArgumentValueResult<{FixTypeName(prop.TypeName, isNullable)}>({counter}, ""{prop.Name}""{defaultValue}))");
            counter++;
        }

        return builder.ToString();
    }

    protected static string GetFunctionArgumentsGetString(TypeBase typeBase)
    {
        var builder = new StringBuilder();
        var counter = 0;
        foreach (var prop in typeBase.Properties)
        {
            var prefix = counter == 0
                ? string.Empty
                : ", ";

            builder.Append($@"{prefix}results.GetValue<{FixTypeName(prop.TypeName, prop.IsNullable)}>(""{prop.Name}"")");
            counter++;
        }

        return builder.ToString();
    }

    private static bool GetRequired(Property property)
    {
        var isOptional = property.TypeName.EndsWith('?')
            || property.IsNullable
            || property.TypeName.StartsWith("System.Nullable", StringComparison.Ordinal);

        return !isOptional;
    }

    private static string FixTypeName(string typeName, bool isNullable)
    {
        var genericArguments = typeName.GetGenericArguments();
        if (!string.IsNullOrEmpty(genericArguments))
        {
            return $"{FixTypeName(typeName.WithoutGenerics(), isNullable)}<{FixTypeName(genericArguments, isNullable && !typeName.StartsWith("System.Nullable"))}>";
        }

        var ns = typeName.GetNamespaceWithDefault();
        if (string.IsNullOrEmpty(ns))
        {
            return typeName;
        }

        var className = typeName.GetClassName();
        if (ns == "ExpressionFramework.CodeGeneration.Models.Abstractions")
        {
            ns = "ExpressionFramework.Core.Abstractions";
        }
        else if (ns == "ExpressionFramework.CodeGeneration.Models.Domains")
        {
            ns = "ExpressionFramework.Core.Domains";
        }
        else if (ns == "ExpressionFramework.CodeGeneration.Models.Evaluatables")
        {
            ns = "ExpressionFramework.Core.Evaluatables";
            className = className.Substring(1); // remove interface prefix
        }
        else if (ns == "ExpressionFramework.CodeGeneration.Models.Operators")
        {
            ns = "ExpressionFramework.Core.Operators";
            className = className.Substring(1); // remove interface prefix
        }

        var suffix = isNullable && className != "Nullable"
            ? "?"
            : string.Empty;

        return $"{ns}.{className}{suffix}";
    }
}
