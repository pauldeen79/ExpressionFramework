namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsersFullyGenerated : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(IsSupportedExpressionForGeneratedParser)
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName($"{x.Name}Parser")
                .WithBaseClass("ExpressionParserBase")
                .AddConstructors(new ClassConstructorBuilder()
                    .AddParameter("parser", typeof(IExpressionParser))
                    .WithChainCall($"base(parser, {x.Name.ReplaceSuffix("Expression", string.Empty, StringComparison.InvariantCulture).CsharpFormat()})")
                )
                .AddMethods(new ClassMethodBuilder()
                    .WithName("DoParse")
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.Expression>")
                    .AddParameter("functionParseResult", typeof(FunctionParseResult))
                    .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                    .WithProtected()
                    .WithOverride()
                    .AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.Expression>.Success(new {Constants.Namespaces.DomainExpressions}.{x.Name}({CreateArguments(x)}));")
                )
            .Build()
            );

    private static string CreateArguments(ITypeBase x)
    {
        var builder = new StringBuilder();
        builder.AppendLine().Append("    ");
        var index = 0;
        foreach (var prop in x.Properties)
        {
            if (index > 0)
            {
                builder.AppendLine(",").Append("    ");
            }
            if (prop.TypeName.GetClassName() == "Expression")
            {
                builder.Append($"new TypedDelegateResultExpression<object>(_ => functionParseResult.GetArgumentValue({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator))");
            }
            else if (prop.TypeName == "ExpressionFramework.Domain.Contracts.ITypedExpression<System.Collections.IEnumerable>")
            {
                builder.Append($"GetTypedExpressionsArgumentValue(functionParseResult, {index}, {prop.Name.CsharpFormat()}, evaluator)");
            }
            else if (prop.TypeName == "System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.Expression>")
            {
                builder.Append($"GetExpressionsArgumentValue(functionParseResult, {index}, {prop.Name.CsharpFormat()}, evaluator)");
            }
            else if (prop.TypeName.WithoutProcessedGenerics().GetClassName() == "ITypedExpression")
            {
                switch (prop.TypeName.GetGenericArguments())
                {
                    case "System.Int32":
                    case "int":
                        builder.Append($"new TypedDelegateResultExpression<int>(_ =>functionParseResult.GetArgumentInt32Value({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, Parser))");
                        break;
                    case "System.Int64":
                    case "long":
                        builder.Append($"new TypedDelegateResultExpression<long>(_ =>functionParseResult.GetArgumentInt64Value({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, Parser))");
                        break;
                    case "System.Boolean":
                    case "bool":
                        builder.Append($"new TypedDelegateResultExpression<bool>(_ =>functionParseResult.GetArgumentBooleanValue({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, Parser))");
                        break;
                    case "System.DateTime":
                    case "DateTime":
                        builder.Append($"new TypedDelegateResultExpression<System.DateTime>(_ =>functionParseResult.GetArgumentDateTimeValue({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, Parser))");
                        break;
                    case "System.Decimal":
                    case "decimal":
                        builder.Append($"new TypedDelegateResultExpression<decimal>(_ =>functionParseResult.GetArgumentDecimalValue({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, Parser))");
                        break;
                    default:
                        builder.Append($"GetArgumentValue<{prop.TypeName.GetGenericArguments()}>(functionParseResult, {index}, {prop.Name.CsharpFormat()}, evaluator)");
                        break;
                }
            }
            else
            {
                throw new NotSupportedException($"Unsupported property type: {prop.TypeName}");
            }
            index++;
        }
        return builder.ToString();
    }
}
