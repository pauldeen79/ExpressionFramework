namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Functions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    public override string Path => Constants.Paths.EvaluatableFunctions;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IEvaluatableBase)))
            .OnSuccess(result =>
                Result.Success(result.Value!.Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.WithoutInterfacePrefix()}Function")
                .AddInterfaces(typeof(ITypedFunction<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IEvaluatable"))
                .AddMethods(
                    new MethodBuilder()
                        .WithName("Evaluate")
                        .AddParameter("context", typeof(FunctionCallContext))
                        .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("System.Object?"))
                        .AddStringCodeStatements("return EvaluateTyped(context).Transform<object?>(x => x);"),
                    new MethodBuilder()
                        .WithName("EvaluateTyped")
                        .AddParameter("context", typeof(FunctionCallContext))
                        .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IEvaluatable"))
                        .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){AddArguments(x)}.Build().OnSuccess(results => {typeof(Result).FullName}.Success<ExpressionFramework.Core.Abstractions.IEvaluatable>(new {x.WithoutInterfacePrefix()}({GetArguments(x)})));")
                )
                .AddAttributes(GetAttributes(x))
                .Build())));

    private static IEnumerable<AttributeBuilder> GetAttributes(TypeBase typeBase)
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

    private static bool GetRequired(Property property)
    {
        var isOptional = property.TypeName.EndsWith('?')
            || property.IsNullable
            || property.TypeName.StartsWith("System.Nullable", StringComparison.Ordinal);

        return !isOptional;
    }

    private static string AddArguments(TypeBase typeBase)
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

    private static string GetArguments(TypeBase typeBase)
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
