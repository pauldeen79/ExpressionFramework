namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract partial class ExpressionFrameworkCSharpClassBase : CSharpClassBase
{
    public CodeGenerationSettings GetSettings(CodeGenerationSettings settings)
        => string.IsNullOrEmpty(LastGeneratedFilesFileName)
            ? settings.ForScaffolding()
            : settings.ForGeneration();

    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string DefaultFileName => string.Empty; // not used because we're using multiple files, but it's abstract so we need to fill ilt

    protected override bool CreateCodeGenerationHeader => true;
    protected override bool EnableNullableContext => true;
    protected override Type RecordCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type RecordConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override string ProjectName => Constants.ProjectName;
    protected override Type BuilderClassCollectionType => typeof(IEnumerable<>);
    protected override bool AddBackingFieldsForCollectionProperties => true;
    protected override bool AddPrivateSetters => true;
    protected override ArgumentValidationType ValidateArgumentsInConstructor => ArgumentValidationType.Shared;

    protected override void FixImmutableBuilderProperty(ClassPropertyBuilder property, string typeName)
    {
        if (typeName.WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            var init = $"{Constants.Namespaces.DomainBuilders}.{nameof(Expressions.ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(source.{{0}})";
            property.ConvertSinglePropertyToBuilderOnBuilder
            (
                $"{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typeName.GetGenericArguments()}>",
                property.IsNullable
                    ? "_{1}Delegate = new (() => source.{0} == null ? null : " + init + ")"
                    : "_{1}Delegate = new (() => " + init + ")"
            );

            AddSinglePropertyBuilderOverload(property);

            if (!property.IsNullable)
            {
                // Allow a default value which implements ITypedExpression<T>, using a default constant value
                property.SetDefaultValueForBuilderClassConstructor(new Literal($"{Constants.Namespaces.DomainBuilders}.{nameof(Expressions.ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(new {Constants.Namespaces.DomainExpressions}.TypedConstantExpression<{typeName.GetGenericArguments()}>({typeName.GetGenericArguments().GetDefaultValue(property.IsNullable)}!))"));
            }
        }
        else if (typeName.WithoutProcessedGenerics().GetClassName() == typeof(IMultipleTypedExpressions<>).WithoutGenerics().GetClassName())
        {
            // This is an ugly hack to transform IMultipleTypedExpression<T> in the code generation model to IEnumerable<ITypedExpression<T>> in the domain model.
            var init = $"{Constants.Namespaces.DomainBuilders}.{nameof(Expressions.ExpressionBuilderFactory)}.CreateTyped<{typeName.GetGenericArguments()}>(x)";
            property.ConvertCollectionPropertyToBuilderOnBuilder
            (
                false,
                RecordConcreteCollectionType.WithoutGenerics(),
                $"{typeof(IEnumerable<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}Builder<{typeName.GetGenericArguments()}>>",
                "{0} = source.{0}.Select(x => " + init + ").ToList()",
                builderCollectionTypeName: BuilderClassCollectionType.WithoutGenerics()
            );
            property.WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{typeof(ITypedExpression<>).WithoutGenerics().GetClassName()}<{typeName.GetGenericArguments()}>>");

            AddEnumerablePropertyBuilderOverload(property);
        }
        else
        {
            base.FixImmutableBuilderProperty(property, typeName);
        }
    }

    private static void AddSinglePropertyBuilderOverload(ClassPropertyBuilder property)
    {
        // Add builder overload that uses T instead of ITypedExpression<T>, and calls the other overload.
        // we need the Value propery of Nullable<T> for value types... (except for predicate expressions, those still have to be injected using ITypedExpression<bool>)
        // for now, we only support int, long and boolean
        var suffix = property.IsNullable && property.TypeName.ToString().GetGenericArguments().In("System.Int32", "System.Int64", "System.Boolean", "int", "long", "bool")
            ? ".Value"
            : string.Empty;
        property.AddBuilderOverload(
            new OverloadBuilder()
                .AddParameter("value", CreateTypeName(property), property.IsNullable)
                .WithInitializeExpression(property.IsNullable
                    ? $"{{2}} = value == null ? null : new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.ToString().GetGenericArguments()}>().WithValue(value{suffix});"
                    : $"{{2}} = new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.ToString().GetGenericArguments()}>().WithValue(value{suffix});"
                ).Build()
            );
    }

    private static void AddEnumerablePropertyBuilderOverload(ClassPropertyBuilder property)
    {
        property.AddBuilderOverload(
            new OverloadBuilder()
                .AddParameter("value", CreateTypeName(property))
                .WithInitializeExpression($"Add{{4}}(new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{CreateTypeName(property)}>().WithValue(value));")
                .Build()
        );
    }

    protected override void Visit<TBuilder, TEntity>(TypeBaseBuilder<TBuilder, TEntity> typeBaseBuilder)
    {
        foreach (var visitor in Visitors)
        {
            visitor.Visit(typeBaseBuilder, _context);
        }
    }

    protected ClassBuilder CreateParserClass(ITypeBase typeBase, string type, string name, string entityNamespace)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.Name}Parser")
            .WithBaseClass($"{type}ParserBase")
            .AddConstructors(
                new ClassConstructorBuilder()
                    .WithChainCall($"base({name.CsharpFormat()})")
            )
            .AddMethods(new ClassMethodBuilder()
                .WithName("DoParse")
                .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{type}>")
                .AddParameter("functionParseResult", typeof(FunctionParseResult))
                .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                .AddParameter("parser", typeof(IExpressionParser))
                .WithProtected()
                .WithOverride()
                .With(parseMethod => AddParseCodeStatements(typeBase, parseMethod, entityNamespace, type))
            )
            .With(x => AddIsSupportedOverride(typeBase, x));

    protected static bool IsSupportedPropertyForGeneratedParser(IClassProperty parserProperty)
        => parserProperty.TypeName.WithoutProcessedGenerics().GetClassName().In(Constants.Types.Expression, Constants.Types.ITypedExpression)
        || parserProperty.TypeName == $"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>"
        || parserProperty.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{Constants.Types.Expression}>";

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

    private static void AddParseCodeStatements(ITypeBase typeBase, ClassMethodBuilder parseMethod, string entityNamespace, string type)
    {
        if (entityNamespace == Constants.Namespaces.DomainExpressions && typeBase.Name.StartsWithAny("TypedConstant", "TypedDelegate"))
        {
            parseMethod.AddLiteralCodeStatements($"return ParseTypedExpression(typeof({typeBase.Name}<>), 0, nameof({typeBase.Name}<{typeof(object).FullName}>.Value), functionParseResult, evaluator, parser);");
            return;
        }

        if (typeBase.Properties.Any(x => !IsSupportedPropertyForGeneratedParser(x)))
        {
            parseMethod.AddLiteralCodeStatements(typeBase.Properties.Select((item, index) => new { Index = index, Item = item }).Where(x => !IsSupportedPropertyForGeneratedParser(x.Item)).Select(x => CreateParseResultVariable(x.Item, x.Index)));
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
                $"    return Result<{Constants.Namespaces.Domain}.{type}>.FromExistingResult(error);",
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
                $"    return Result<{Constants.Namespaces.Domain}.{type}>.FromExistingResult(typeResult);",
                "}"
            );
        }

        var initializer = typeBase.GenericTypeArguments.Count switch
        {
            0 => $"new {entityNamespace}.{typeBase.Name}({CreateParseArguments(typeBase, type)})",
            1 => $"({Constants.Namespaces.Domain}.{type})Activator.CreateInstance(typeof({entityNamespace}.{typeBase.Name}<>).MakeGenericType(typeResult.Value!))",
            _ => throw new NotSupportedException("Expressions with multiple generic type arguments are not supported")
        };

        parseMethod.AddLiteralCodeStatements("#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.");
        parseMethod.AddLiteralCodeStatements($"return Result<{Constants.Namespaces.Domain}.{type}>.Success({initializer});");
        parseMethod.AddLiteralCodeStatements("#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.");
    }

    private static string CreateParseResultVariable(IClassProperty prop, int index)
    {
        var defaultValueSuffix = prop.IsNullable
            ? ", default"
            : string.Empty;

        var genericType = GetCustomType(prop.TypeName.GetGenericArguments());
        if (prop.TypeName.WithoutProcessedGenerics().GetClassName() == "ITypedExpression" && !string.IsNullOrEmpty(genericType.MethodType))
        {
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgument{genericType.MethodType}ValueResult({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else if (prop.TypeName == typeof(object).FullName || prop.TypeName == "object")
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
            var typeName = prop.TypeName.WithoutProcessedGenerics() switch
            {
                "System.Nullable" => prop.TypeName.GetGenericArguments(),
                "System.Collections.Generic.IReadOnlyCollection" => $"{typeof(IEnumerable<>).WithoutGenerics()}<{prop.TypeName.GetGenericArguments()}>",
                _ => prop.TypeName
            };
            return $"var {prop.Name.ToPascalCase()}Result = functionParseResult.GetArgumentExpressionResult<{typeName}>({index}, {prop.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
    }

    private static string CreateParseArguments(ITypeBase typeBase, string type)
    {
        var builder = new StringBuilder();
        builder.AppendLine().Append("    ");
        var index = 0;
        foreach (var property in typeBase.Properties)
        {
            CreateParseArgument(builder, index, property, type);
            index++;
        }

        return builder.ToString();
    }

    private static void CreateParseArgument(StringBuilder builder, int index, IClassProperty property, string type)
    {
        var defaultValueSuffix = property.IsNullable
            ? ", default"
            : string.Empty;

        var nullableSuffix = property.IsNullable
            ? "?"
            : string.Empty;

        var nullableBangSuffix = property.IsNullable
            ? string.Empty
            : "!";

        if (index > 0)
        {
            builder.AppendLine(",").Append("    ");
        }

        if (property.TypeName.GetClassName() == Constants.Types.Expression)
        {
            builder.Append($"new TypedConstantResultExpression<{typeof(object).FullName}{nullableSuffix}>(functionParseResult.GetArgumentValueResult({index}, {property.Name.CsharpFormat()}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
        }
        else if (property.TypeName == $"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>")
        {
            builder.Append($"functionParseResult.GetTypedExpressionsArgumentValueExpression({index}, {property.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{type}>")
        {
            builder.Append($"functionParseResult.GetExpressionsArgumentValueResult({index}, {property.Name.CsharpFormat()}, evaluator, parser)");
        }
        else if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            var genericType = GetCustomType(property.TypeName.GetGenericArguments());
            if (!string.IsNullOrEmpty(genericType.MethodType))
            {
                builder.Append($"functionParseResult.GetArgument{genericType.MethodType}ValueExpression({index}, {property.Name.CsharpFormat()}, evaluator, parser{defaultValueSuffix})");
            }
            else
            {
                builder.Append($"functionParseResult.GetArgumentValueExpression<{property.TypeName.GetGenericArguments()}>({index}, {property.Name.CsharpFormat()}, evaluator, parser{defaultValueSuffix})");
            }
        }
        else
        {
            builder.Append($"{property.Name.ToPascalCase()}Result.Value{nullableBangSuffix}");
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
            "System.String" or "string" => ("string", "String"),
            _ => (string.Empty, string.Empty)
        };

    private static string CreateTypeName(ClassPropertyBuilder builder)
    {
        if (builder.TypeName.ToString().WithoutProcessedGenerics().GetClassName() == typeof(ITypedExpression<>).WithoutGenerics().GetClassName())
        {
            if (builder.Name.ToString() == Constants.ArgumentNames.PredicateExpression)
            {
                // hacking here... we only want to allow to inject the typed expression
                return builder.TypeName.ToString();
            }
            else
            {
                return builder.TypeName.ToString().GetGenericArguments();
            }
        }

        if (builder.TypeName.ToString().GetGenericArguments().StartsWith($"{Constants.Namespaces.DomainContracts}.ITypedExpression"))
        {
            return builder.TypeName.ToString().GetGenericArguments().GetGenericArguments();
        }

        if (builder.TypeName.ToString().GetClassName() == Constants.Types.Expression)
        {
            // note that you might expect to check for the nullability of the property, but the Expression itself may be required although it's evaluation can result in null
            return $"{typeof(object).FullName}?";
        }

        return builder.TypeName.ToString();
    }

    private static readonly VisitorContext _context = new();

    private static readonly IVisitor[] Visitors = typeof(ExpressionFrameworkCSharpClassBase).Assembly.GetExportedTypes().Where(t => typeof(IVisitor).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
        .Select(Activator.CreateInstance)
        .Cast<IVisitor>()
        .ToArray();
}
