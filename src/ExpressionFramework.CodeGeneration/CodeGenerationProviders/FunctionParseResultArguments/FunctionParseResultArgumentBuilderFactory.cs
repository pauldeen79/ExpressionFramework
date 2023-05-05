namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.FunctionParseResultArguments;

[ExcludeFromCodeCoverage]
public class FunctionParseResultArgumentBuilderFactory : FunctionParseResultArgumentsBase
{
    public override object CreateModel()
        => CreateBuilderFactoryModels(
            new[] { typeof(LiteralArgument).ToClass(CreateClassSettings()), typeof(FunctionArgument).ToClass(CreateClassSettings()) },
            new(
                CurrentNamespace,
                nameof(FunctionParseResultArgumentBuilderFactory),
                typeof(FunctionParseResultArgument).FullName!,
                CurrentNamespace,
                "FunctionParseResultArgumentBuilder",
                ProjectName
            )
        );
}
