namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class ExpressionFrameworkCSharpClassBase : CsharpClassGeneratorPipelineCodeGenerationProviderBase
{
    protected ExpressionFrameworkCSharpClassBase(IMediator mediator, ICsharpExpressionDumper csharpExpressionDumper) : base(mediator, csharpExpressionDumper)
    {
    }

    public override bool RecurseOnDeleteGeneratedFiles => false;
    public override string LastGeneratedFilesFilename => string.Empty;
    public override Encoding Encoding => Encoding.UTF8;

    protected override Type EntityCollectionType => typeof(IReadOnlyCollection<>);
    protected override Type EntityConcreteCollectionType => typeof(ReadOnlyValueCollection<>);
    protected override Type BuilderCollectionType => typeof(ObservableCollection<>);

    protected override string ProjectName => "ExpressionFramework";
    protected override string CoreNamespace => "ExpressionFramework.Domain";
    protected override bool CopyAttributes => true;
    protected override bool CopyInterfaces => true;
    protected override bool CreateRecord => true;
    protected override bool GenerateMultipleFiles => false;

    protected override bool SkipNamespaceOnTypenameMappings(string @namespace)
        => @namespace == $"{CodeGenerationRootNamespace}.Models.Contracts";

    protected override IEnumerable<TypenameMappingBuilder> CreateAdditionalTypenameMappings()
    {
        yield return new TypenameMappingBuilder()
            .WithSourceTypeName(typeof(ITypedExpression<>).WithoutGenerics())
            .WithTargetTypeName("ExpressionFramework.Domain.Contracts.ITypedExpression");
        yield return new TypenameMappingBuilder()
            .WithSourceTypeName("ExpressionFramework.Domain.Contracts.ITypedExpression")
            .WithTargetTypeName("ExpressionFramework.Domain.Contracts.ITypedExpression")
            .AddMetadata
            (
                new MetadataBuilder().WithValue($"{CoreNamespace}.Contracts").WithName(MetadataNames.CustomBuilderNamespace),
                new MetadataBuilder().WithValue("{TypeName.ClassName.NoGenerics}Builder{TypeName.GenericArgumentsWithBrackets}").WithName(MetadataNames.CustomBuilderName),
                new MetadataBuilder().WithValue($"{CoreNamespace}.Contracts").WithName(MetadataNames.CustomBuilderInterfaceNamespace),
                new MetadataBuilder().WithValue("{TypeName.ClassName.NoGenerics}Builder{TypeName.GenericArgumentsWithBrackets}").WithName(MetadataNames.CustomBuilderInterfaceName),
                new MetadataBuilder().WithValue("[Name][NullableSuffix].ToBuilder()[ForcedNullableSuffix]").WithName(MetadataNames.CustomBuilderSourceExpression),
                new MetadataBuilder().WithValue(new Literal("new ExpressionFramework.Domain.Builders.Expressions.TypedConstantExpressionBuilder{TypeName.GenericArgumentsWithBrackets}()", null)).WithName(MetadataNames.CustomBuilderDefaultValue),
                new MetadataBuilder().WithValue("[Name][NullableSuffix].Build()[ForcedNullableSuffix]").WithName(MetadataNames.CustomBuilderMethodParameterExpression),
                new MetadataBuilder().WithName(MetadataNames.CustomEntityInterfaceTypeName).WithValue($"{CoreNamespace}.Contracts.ITypedExpression")
            );

        //HACK for wrong detection of nullability of multiple or nested generic arguments
        yield return new TypenameMappingBuilder()
            .WithSourceTypeName("ExpressionFramework.CodeGeneration.Models.Contracts.ITypedExpression<System.Collections.Generic.IEnumerable<System.Object>>")
            .WithTargetTypeName("ExpressionFramework.CodeGeneration.Models.Contracts.ITypedExpression<System.Collections.Generic.IEnumerable<System.Object>>")
            .AddMetadata(new MetadataBuilder().WithName(MetadataNames.CustomTypeName).WithValue("ExpressionFramework.CodeGeneration.Models.Contracts.ITypedExpression<System.Collections.Generic.IEnumerable<System.Object?>>"));

        yield return new TypenameMappingBuilder()
            .WithSourceTypeName("System.Func<System.Object?,System.Object>")
            .WithTargetTypeName("System.Func<System.Object?,System.Object>")
            .AddMetadata(new MetadataBuilder().WithName(MetadataNames.CustomTypeName).WithValue("System.Func<System.Object?,System.Object?>"));

        yield return new TypenameMappingBuilder()
            .WithSourceTypeName("System.Func<System.Object?,CrossCutting.Common.Results.Result<System.Object>>")
            .WithTargetTypeName("System.Func<System.Object?,CrossCutting.Common.Results.Result<System.Object>>")
            .AddMetadata(new MetadataBuilder().WithName(MetadataNames.CustomTypeName).WithValue("System.Func<System.Object?,CrossCutting.Common.Results.Result<System.Object?>>"));
    }

    protected override bool IsAbstractType(Type type)
    {
        type = type.IsNotNull(nameof(type));

        if (type.IsInterface && type.Namespace == $"{CodeGenerationRootNamespace}.Models" && type.Name.Substring(1).In(Constants.Types.Aggregator, Constants.Types.Evaluatable, Constants.Types.Expression, Constants.Types.Operator))
        {
            return true;
        }
        return base.IsAbstractType(type);
    }

    protected ClassBuilder CreateParserClass(TypeBase typeBase, string type, string name, string entityNamespace, PipelineSettings settings)
        => new ClassBuilder()
            .WithNamespace(CurrentNamespace)
            .WithName($"{typeBase.WithoutInterfacePrefix()}Parser")
            .WithBaseClass($"{type}ParserBase")
            .AddConstructors(
                new ConstructorBuilder()
                    .WithChainCall($"base({CsharpExpressionDumper.Dump(name)})")
            )
            .AddMethods(new MethodBuilder()
                .WithName("DoParse")
                .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{type}>")
                .AddParameter("functionParseResult", typeof(FunctionParseResult))
                .AddParameter("evaluator", typeof(IFunctionParseResultEvaluator))
                .AddParameter("parser", typeof(IExpressionParser))
                .WithProtected()
                .WithOverride()
                .With(parseMethod => AddParseCodeStatements(typeBase, parseMethod, entityNamespace, type, settings))
            )
            .With(x => AddIsSupportedOverride(typeBase, x));

    protected PipelineSettings CreateSettings()
        => new PipelineSettingsBuilder()
            .AddTypenameMappings(CreateTypenameMappings())
            .AddNamespaceMappings(CreateNamespaceMappings())
            .Build();

    protected static bool IsSupportedPropertyForGeneratedParser(Property parserProperty)
        => parserProperty.TypeName.WithoutProcessedGenerics().GetClassName().In($"I{Constants.Types.Expression}", Constants.Types.ITypedExpression)
        || parserProperty.TypeName == $"{Constants.CodeGenerationRootNamespace}.Contracts.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>"
        || parserProperty.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.CodeGenerationRootNamespace}.Models.I{Constants.Types.Expression}>";

    private static void AddIsSupportedOverride(TypeBase model, ClassBuilder parserClass)
    {
        if (model.GenericTypeArguments.Count > 0)
        {
            parserClass.AddMethods(
                new MethodBuilder()
                    .WithProtected()
                    .WithOverride()
                    .WithReturnType(typeof(bool))
                    .WithName("IsNameValid")
                    .AddParameter("functionName", typeof(string))
                    .AddStringCodeStatements("return base.IsNameValid(functionName.WithoutGenerics());"));
        }
    }

    private void AddParseCodeStatements(TypeBase typeBase, MethodBuilder parseMethod, string entityNamespace, string type, PipelineSettings settings)
    {
        if (entityNamespace == Constants.Namespaces.DomainExpressions && typeBase.GenericTypeArguments.Count > 0 && typeBase.Properties.Count == 1)
        {
            parseMethod.AddStringCodeStatements($"return ParseTypedExpression(typeof({typeBase.WithoutInterfacePrefix()}<>), 0, {CsharpExpressionDumper.Dump(typeBase.Properties.First().Name)}, functionParseResult, evaluator, parser);");
            return;
        }

        if (typeBase.Properties.Any(x => !IsSupportedPropertyForGeneratedParser(x)))
        {
            parseMethod.AddStringCodeStatements(typeBase.Properties.Select((item, index) => new { Index = index, Item = item }).Where(x => !IsSupportedPropertyForGeneratedParser(x.Item)).Select(x => CreateParseResultVariable(x.Item, x.Index, settings)));
            parseMethod.AddStringCodeStatements
            (
                "var error = new Result[]",
                "{"
            );
            parseMethod.AddStringCodeStatements(typeBase.Properties.Where(x => !IsSupportedPropertyForGeneratedParser(x)).Select(x => $"    {x.Name.ToPascalCase(CultureInfo.InvariantCulture)}Result,"));
            parseMethod.AddStringCodeStatements
            (
                "}.FirstOrDefault(x => !x.IsSuccessful());",
                "if (error != null)",
                "{",
                $"    return Result.FromExistingResult<{Constants.Namespaces.Domain}.{type}>(error);",
                "}"
            );
        }

        if (typeBase.GenericTypeArguments.Count == 1)
        {
            parseMethod.AddStringCodeStatements
            (
                "var typeResult = functionParseResult.FunctionName.GetGenericTypeResult();",
                "if (!typeResult.IsSuccessful())",
                "{",
                $"    return Result.FromExistingResult<{Constants.Namespaces.Domain}.{type}>(typeResult);",
                "}"
            );
        }

        var initializer = typeBase.GenericTypeArguments.Count switch
        {
            0 => $"new {entityNamespace}.{typeBase.WithoutInterfacePrefix()}({CreateParseArguments(typeBase, type)})",
            1 => $"({Constants.Namespaces.Domain}.{type}){typeof(Activator).FullName}.{nameof(Activator.CreateInstance)}(typeof({entityNamespace}.{typeBase.WithoutInterfacePrefix()}<>).MakeGenericType(typeResult.Value!))",
            _ => throw new NotSupportedException("Expressions with multiple generic type arguments are not supported")
        };

        parseMethod.AddStringCodeStatements("#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.");
        parseMethod.AddStringCodeStatements($"return Result.Success<{Constants.Namespaces.Domain}.{type}>({initializer});");
        parseMethod.AddStringCodeStatements("#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.");
    }

    private string CreateParseResultVariable(Property property, int index, PipelineSettings settings)
    {
        var defaultValueSuffix = property.IsNullable
            ? ", default"
            : string.Empty;

        var genericType = GetCustomType(property.TypeName.GetGenericArguments());
        if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression && !string.IsNullOrEmpty(genericType.MethodType))
        {
            return $"var {property.Name.ToPascalCase(CultureInfo.InvariantCulture)}Result = functionParseResult.GetArgument{genericType.MethodType}ValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else if (property.TypeName == typeof(object).FullName || property.TypeName == "object")
        {
            return $"var {property.Name.ToPascalCase(CultureInfo.InvariantCulture)}Result = functionParseResult.GetArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
        else
        {
            var typeName = property.TypeName.WithoutProcessedGenerics() switch
            {
                "System.Nullable" => property.TypeName.GetGenericArguments().MapTypeName(settings),
                "System.Collections.Generic.IReadOnlyCollection" => $"{typeof(IEnumerable<>).WithoutGenerics()}<{property.TypeName.GetGenericArguments().MapTypeName(settings)}>",
                _ => property.TypeName.MapTypeName(settings)
            };
            return $"var {property.Name.ToPascalCase(CultureInfo.InvariantCulture)}Result = functionParseResult.GetArgumentExpressionResult<{typeName}>({index}, {CsharpExpressionDumper.Dump(property.Name)}, functionParseResult.Context, evaluator, parser{defaultValueSuffix});";
        }
    }

    private string CreateParseArguments(TypeBase typeBase, string type)
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

    private void CreateParseArgument(StringBuilder builder, int index, Property property, string type)
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

        if (property.TypeName.GetClassName() == $"I{Constants.Types.Expression}")
        {
            builder.Append($"new TypedConstantResultExpression<{typeof(object).FullName}{nullableSuffix}>(functionParseResult.GetArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}, functionParseResult.Context, evaluator, parser{defaultValueSuffix}))");
        }
        else if (property.TypeName == $"{typeof(ITypedExpression<>).WithoutGenerics()}<{typeof(IEnumerable).FullName}>")
        {
            builder.Append($"functionParseResult.GetTypedExpressionsArgumentValueExpression({index}, {CsharpExpressionDumper.Dump(property.Name)}, evaluator, parser)");
        }
        else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.CodeGenerationRootNamespace}.Models.I{type}>")
        {
            builder.Append($"functionParseResult.GetExpressionsArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}, evaluator, parser)");
        }
        else if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            var genericType = GetCustomType(property.TypeName.GetGenericArguments());
            if (!string.IsNullOrEmpty(genericType.MethodType))
            {
                builder.Append($"functionParseResult.GetArgument{genericType.MethodType}ValueExpression({index}, {CsharpExpressionDumper.Dump(property.Name)}, evaluator, parser{defaultValueSuffix})");
            }
            else
            {
                builder.Append($"functionParseResult.GetArgumentValueExpression<{property.TypeName.GetGenericArguments()}>({index}, {CsharpExpressionDumper.Dump(property.Name)}, evaluator, parser{defaultValueSuffix})");
            }
        }
        else
        {
            builder.Append($"{property.Name.ToPascalCase(CultureInfo.InvariantCulture)}Result.Value{nullableBangSuffix}");
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
}
