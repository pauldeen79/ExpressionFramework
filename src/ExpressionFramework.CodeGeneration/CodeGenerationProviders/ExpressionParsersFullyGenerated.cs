namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionParsersFullyGenerated : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserExpressionResultParsers;

    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Where(x => !x.Name.StartsWithAny("TypedConstant", "TypedDelegate", "Delegate"))
            .Select(model => CreateParserClass
            (
                model,
                Constants.Types.Expression,
                model.Name.ReplaceSuffix(Constants.Types.Expression, string.Empty, StringComparison.InvariantCulture),
                parseMethod => AddCodeStatements(model, parseMethod),
                parserClass => AddIsSupportedOverride(model, parserClass)
            ).Build());

    private static void AddIsSupportedOverride(ITypeBase model, ClassBuilder parserClass)
    {
        if (model.GenericTypeArguments.Count > 0)
        {
            parserClass.AddMethods(
                new ClassMethodBuilder()
                    .WithProtected()
                    .WithOverride()
                    .WithType(typeof(bool))
                    .WithName("IsNameValid")
                    .AddParameter("functionName", typeof(string))
                    .AddLiteralCodeStatements("return base.IsNameValid(functionName.WithoutGenerics());"));
        }
    }

    private static void AddCodeStatements(ITypeBase typeBase, ClassMethodBuilder parseMethod)
    {
        if (typeBase.Properties.Any(x => !IsSupportedPropertyForGeneratedParser(x)))
        {
            parseMethod.AddLiteralCodeStatements(typeBase.Properties.Select((item, index) => new { Index = index, Item = item }).Where(x => !IsSupportedPropertyForGeneratedParser(x.Item)).Select(x => CreateResultVariable(x.Item, x.Index)));
            parseMethod.AddLiteralCodeStatements
            (
                "var error = new Result[]",
                "{"
            );
            parseMethod.AddLiteralCodeStatements(typeBase.Properties.Where(x => !IsSupportedPropertyForGeneratedParser(x)).Select(x => $"    {x.Name.ToPascalCase()}Result,"));
            parseMethod.AddLiteralCodeStatements
            (
                "}.FirstOrDefault(x => !x.IsSuccessful());",
                "if (error != null)",
                "{",
                $"    return Result<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>.FromExistingResult(error);",
                "}"
            );
        }

        if (typeBase.GenericTypeArguments.Count == 1)
        {
            parseMethod.AddLiteralCodeStatements
            (
                "var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();",
                "if (!typeResult.IsSuccessful())",
                "{",
                $"    return Result<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>.FromExistingResult(typeResult);",
                "}"
            );
        }

        var initializer = typeBase.GenericTypeArguments.Count switch
        {
            0 => $"new {Constants.Namespaces.DomainExpressions}.{typeBase.Name}({CreateArguments(typeBase)})",
            1 => $"(Expression)Activator.CreateInstance(typeof({Constants.Namespaces.DomainExpressions}.{typeBase.Name}<>).MakeGenericType(typeResult.Value!))",
            _ => throw new NotSupportedException("Expressions with multiple generic type arguments are not supported")
        };

        parseMethod.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>.Success({initializer});");
    }

    private static string CreateResultVariable(IClassProperty prop, int index)
    {
        var defaultValueSuffix = prop.IsNullable
            ? ", default"
            : string.Empty;

        var genericType = GetCustomType(prop.TypeName.GetGenericArguments());
        if (!string.IsNullOrEmpty(genericType.MethodType))
        {
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgument{genericType.MethodType}ValueResult({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else if (prop.TypeName == "System.Object" || prop.TypeName == "object")
        {
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgumentValueResult({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else if (prop.TypeName.WithoutProcessedGenerics().GetClassName() == typeof(IMultipleTypedExpressions<>).WithoutGenerics().GetClassName())
        {
            // This is an ugly hack to transform IMultipleTypedExpression<T> in the code generation model to IEnumerable<ITypedExpression<T>> in the domain model.
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgumentExpressionResult<{$"{typeof(IEnumerable<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{prop.TypeName.GetGenericArguments()}>>"}>({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else
        {
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgumentExpressionResult<{prop.TypeName}>({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
    }

    private static string CreateArguments(ITypeBase x)
    {
        var builder = new StringBuilder();
        builder.AppendLine().Append("    ");
        var index = 0;
        foreach (var property in x.Properties)
        {
            CreateArgument(builder, index, property);
            index++;
        }

        return builder.ToString();
    }

    private static void CreateArgument(StringBuilder builder, int index, IClassProperty property)
    {
        var defaultValueSuffix = property.IsNullable
            ? ", default"
            : string.Empty;

        if (index > 0)
        {
            builder.AppendLine(",").Append("    ");
        }

        if (property.TypeName.GetClassName() == Constants.Types.Expression)
        {
            builder.Append($"new TypedConstantResultExpression<object>(functionParseResult.GetArgumentValueResult({index}, {property.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
        }
        else if (property.TypeName == $"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>")
        {
            builder.Append($"functionParseResult.GetTypedExpressionsArgumentValueExpression({index}, {property.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>")
        {
            builder.Append($"functionParseResult.GetExpressionsArgumentValueResult({index}, {property.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            var genericType = GetCustomType(property.TypeName.GetGenericArguments());
            if (string.IsNullOrEmpty(genericType.ClrType))
            {
                builder.Append($"functionParseResult.GetArgumentValueExpression<{property.TypeName.GetGenericArguments()}>({index}, {property.Name.CsharpFormat()}, evaluator, parser{defaultValueSuffix})");
            }
            else
            {
                builder.Append($"new TypedConstantResultExpression<{genericType.ClrType}>(functionParseResult.GetArgument{genericType.MethodType}ValueResult({index}, {property.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
            }
        }
        else
        {
            builder.Append($"{property.Name.ToPascalCase()}Result.Value");
        }
    }

    private static (string ClrType, string MethodType) GetCustomType(string genericArguments)
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
