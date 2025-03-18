namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class Functions(IPipelineService pipelineService) : ExpressionFrameworkCSharpClassBase(pipelineService)
{
    private static readonly string[] StringComparisonKeywords = ["StartsWith", "EndsWith", "String"];
    private const string ContextParameterName = "context";

    public override string Path => Constants.Paths.OperatorFunctions;

    public override async Task<Result<IEnumerable<TypeBase>>> GetModel(CancellationToken cancellationToken)
        => (await GetOverrideModels(typeof(IOperatorBase)))
            .OnSuccess(result =>
                Result.Success(result.Value!.SelectMany(x => new TypeBase[] { CreateOperatorFunction(x), CreateOperatorEvaluationFunction(x) })));

    private TypeBase CreateOperatorFunction(TypeBase typeBase)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.WithoutInterfacePrefix()}Function")
            .AddInterfaces(typeof(ITypedFunction<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IOperator"))
            .AddMethods(
                new MethodBuilder()
                    .WithName("Evaluate")
                    .AddParameter(ContextParameterName, typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("System.Object?"))
                    .AddStringCodeStatements("return EvaluateTyped(context).Transform<object?>(x => x);"),
                new MethodBuilder()
                    .WithName("EvaluateTyped")
                    .AddParameter(ContextParameterName, typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("ExpressionFramework.Core.Abstractions.IOperator"))
                    .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){GetFunctionArgumentsAddString(typeBase)}.Build().OnSuccess(results => {typeof(Result).FullName}.Success<ExpressionFramework.Core.Abstractions.IOperator>(new {typeBase.WithoutInterfacePrefix()}({GetFunctionArgumentsGetString(typeBase)})));")
            )
            .AddAttributes(GetFunctionAttributes(typeBase))
            .Build();

    private TypeBase CreateOperatorEvaluationFunction(TypeBase typeBase)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.WithoutInterfacePrefix().ReplaceSuffix("Operator", string.Empty, StringComparison.Ordinal)}Function")
            .AddInterfaces(typeof(ITypedFunction<>).ReplaceGenericTypeName(typeof(bool)))
            .AddMethods(
                new MethodBuilder()
                    .WithName("Evaluate")
                    .AddParameter(ContextParameterName, typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName("System.Object?"))
                    .AddStringCodeStatements("return EvaluateTyped(context).Transform<object?>(x => x);"),
                new MethodBuilder()
                    .WithName("EvaluateTyped")
                    .AddParameter(ContextParameterName, typeof(FunctionCallContext))
                    .WithReturnTypeName(typeof(Result<>).ReplaceGenericTypeName(typeof(bool)))
                    .AddStringCodeStatements($"return new {typeof(ResultDictionaryBuilder).FullName}(){GetOperatorArgumentsAddString()}.Build().OnSuccess(results => new {typeBase.WithoutInterfacePrefix()}().Evaluate({GetOperatorArgumentsGetString()}));")

            )
            .AddAttributes(CreateOperatorAttributes(typeBase))
            .Build();

    private static IEnumerable<AttributeBuilder> CreateOperatorAttributes(TypeBase typeBase)
    {
        yield return new AttributeBuilder()
            .WithName(typeof(FunctionArgumentAttribute))
            .AddParameters
            (
                new AttributeParameterBuilder().WithValue("LeftValue"),
                new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({typeof(object).FullName})")),
                new AttributeParameterBuilder().WithValue(true)
            );

        yield return new AttributeBuilder()
            .WithName(typeof(FunctionArgumentAttribute))
            .AddParameters
            (
                new AttributeParameterBuilder().WithValue("RightValue"),
                new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({typeof(object).FullName})")),
                new AttributeParameterBuilder().WithValue(true)
            );

        if (StringComparisonKeywords.Any(x => typeBase.Name.Contains(x, StringComparison.Ordinal)))
        {
            yield return new AttributeBuilder()
                .WithName(typeof(FunctionArgumentAttribute))
                .AddParameters
                (
                    new AttributeParameterBuilder().WithValue("StringComparison"),
                    new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({typeof(StringComparison).FullName})")),
                    new AttributeParameterBuilder().WithValue(false)
                );
        }
    }

    protected static string GetOperatorArgumentsAddString()
        => GetFunctionArgumentsAddString(new ClassBuilder()
            .WithName("Dummy")
            .AddProperties(CreateOperatorAttributesArgumentsProperties())
            .Build());

    protected static string GetOperatorArgumentsGetString()
        => GetFunctionArgumentsGetString(new ClassBuilder()
            .WithName("Dummy")
            .AddProperties(CreateOperatorAttributesArgumentsProperties())
            .Build());

    private static IEnumerable<PropertyBuilder> CreateOperatorAttributesArgumentsProperties()
    {
        yield return new PropertyBuilder()
            .WithName("LeftValue")
            .WithType(typeof(object))
            .WithIsNullable();

        yield return new PropertyBuilder()
            .WithName("RightValue")
            .WithType(typeof(object))
            .WithIsNullable();

        yield return new PropertyBuilder()
            .WithName("stringComparison")
            .WithType(typeof(StringComparison));
    }
}
