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
        => typeBase.Properties.Select(x => new AttributeBuilder()
            .WithName(typeof(FunctionArgumentAttribute))
            .AddParameters(
                new AttributeParameterBuilder().WithValue(x.Name),
                new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({FixTypeName(x.TypeName)})")),
                new AttributeParameterBuilder().WithValue(x.IsNullable) //TODO: Detect nullability correctly
            ));

    private static string AddArguments(TypeBase typeBase)
    {
        var builder = new StringBuilder();
        var counter = 0;
        foreach (var prop in typeBase.Properties)
        {
            builder.Append(@$".Add(""{prop.Name}"", () => context.GetArgumentValueResult<{FixTypeName(prop.TypeName)}>({counter}, ""{prop.Name}""))");
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

            builder.Append($@"{prefix}results.GetValue<{FixTypeName(prop.TypeName)}>(""{prop.Name}"")");
            counter++;
        }

        return builder.ToString();
    }

    private static string FixTypeName(string typeName)
    {
        var genericArguments = typeName.GetGenericArguments();
        if (!string.IsNullOrEmpty(genericArguments))
        {
            return $"{FixTypeName(typeName.WithoutGenerics())}<{FixTypeName(genericArguments)}>";
        }

        var ns = typeName.GetNamespaceWithDefault();
        if (string.IsNullOrEmpty(ns))
        {
            return ns;
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
        
        return $"{ns}.{className}";
    }
}
