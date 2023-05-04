namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsersFullyGenerated : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(IsSupportedExpressionForGeneratedParser)
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Expression,
                x.Name.ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                m => m.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>.Success(new {Constants.Namespaces.DomainExpressions}.{x.Name}({CreateArguments(x)}));")
            ).Build());

    private static string CreateArguments(ITypeBase x)
    {
        var builder = new StringBuilder();
        builder.AppendLine().Append("    ");
        var index = 0;
        foreach (var prop in x.Properties)
        {
            CreateArgument(builder, index, prop);
            index++;
        }
        return builder.ToString();
    }

    private static void CreateArgument(StringBuilder builder, int index, IClassProperty prop)
    {
        var defaultValueSuffix = prop.IsNullable ? ", default" : string.Empty;
        if (index > 0)
        {
            builder.AppendLine(",").Append("    ");
        }
        if (prop.TypeName.GetClassName() == Constants.Types.Expression)
        {
            builder.Append($"new TypedConstantResultExpression<object>(functionParseResult.GetArgumentValueResult({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
        }
        else if (prop.TypeName == $"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>")
        {
            builder.Append($"functionParseResult.GetTypedExpressionsArgumentValueResult({index}, {prop.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (prop.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>")
        {
            builder.Append($"functionParseResult.GetExpressionsArgumentValueResult({index}, {prop.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (prop.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            var genericType = GetGenericType(prop.TypeName.GetGenericArguments());
            if (string.IsNullOrEmpty(genericType.ClrType))
            {
                builder.Append($"functionParseResult.GetArgumentValueResult<{prop.TypeName.GetGenericArguments()}>({index}, {prop.Name.CsharpFormat()}, evaluator, parser{defaultValueSuffix})");
            }
            else
            {
                builder.Append($"new TypedConstantResultExpression<{genericType.ClrType}>(functionParseResult.GetArgument{genericType.MethodType}ValueResult({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
            }
        }
        else
        {
            throw new NotSupportedException($"Unsupported property type: {prop.TypeName}");
        }
    }

    private static (string ClrType, string MethodType) GetGenericType(string genericArguments)
        => genericArguments switch
        {
            "System.Int32" or "int" => ("int", "Int32"),
            "System.Int64" or "long" => ("long", "Int64"),
            "System.Boolean" or "bool" => ("bool", "Boolean"),
            "System.DateTime" or "DateTime" => ("System.DateTime", "DateTime"),
            "System.Decimal" or "decimal" => ("decimal", "Decimal"),
            _ => (string.Empty, string.Empty)
        };
}
