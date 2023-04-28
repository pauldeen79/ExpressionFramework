namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserFunctionResultParsers;
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(x => !x.Name.StartsWith("TypedDelegate"))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .AddInterfaces(typeof(IFunctionResultParser))
                .AddInterfaces($"{Constants.Namespaces.Parser}.Contracts.IExpressionResolver")
                .AddMethods(
                    new ClassMethodBuilder()
                        .WithName(nameof(IFunctionResultParser.Parse))
                        .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                        .AddParameter("functionParseResult", typeof(FunctionParseResult))
                        .AddParameter("context", typeof(object), isNullable: true)
                        .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                        .AddLiteralCodeStatements
                        (
                            "var result = Parse(functionParseResult, evaluator);",
                            $"if (!result.IsSuccessful() || result.Status == {typeof(ResultStatus).FullName}.Continue)",
                            "{",
                            "    return Result<object?>.FromExistingResult(result);",
                            "}",
                            "return result.Value!.Evaluate(context);"
                        ),
                    new ClassMethodBuilder()
                        .WithName("Parse")
                        .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Expression>")
                        .AddParameter("functionParseResult", typeof(FunctionParseResult))
                        .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                        .AddLiteralCodeStatements(
                            $"if (functionParseResult.{nameof(FunctionParseResult.FunctionName)}.ToUpperInvariant() != \"{x.Name[..^10].ToUpperInvariant()}\")",
                            "{",
                            $"    return {typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Expression>.Continue();",
                            "}"
                        )
                        .AddNotImplementedException()
                )
            .AddFields(
                new ClassFieldBuilder()
                    .WithName("_parser")
                    .WithType(typeof(IExpressionParser))
                    .WithReadOnly()
            )
            .AddConstructors(
                new ClassConstructorBuilder()
                    .AddParameter("parser", typeof(IExpressionParser))
                    .AddLiteralCodeStatements("_parser = parser;")
            )
            .Build()
            );
}
