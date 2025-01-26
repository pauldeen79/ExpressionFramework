namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public abstract class ExpressionFrameworkCSharpClassBase(IPipelineService pipelineService, ICsharpExpressionDumper csharpExpressionDumper) : CsharpClassGeneratorPipelineCodeGenerationProviderBase(pipelineService)
{
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
    protected override bool EnableGlobalUsings => true;

    protected ICsharpExpressionDumper CsharpExpressionDumper { get; } = csharpExpressionDumper;

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
                new MetadataBuilder().WithValue("{NoGenerics(ClassName($property.TypeName))}Builder{GenericArguments($property.TypeName, true)}").WithName(MetadataNames.CustomBuilderName),
                new MetadataBuilder().WithValue($"{CoreNamespace}.Contracts").WithName(MetadataNames.CustomBuilderInterfaceNamespace),
                new MetadataBuilder().WithValue("{NoGenerics(ClassName($property.TypeName))}Builder{GenericArguments($property.TypeName, true)}").WithName(MetadataNames.CustomBuilderInterfaceName),
                new MetadataBuilder().WithValue("[Name][NullableSuffix].ToBuilder()[ForcedNullableSuffix]").WithName(MetadataNames.CustomBuilderSourceExpression),
                new MetadataBuilder().WithValue(new Literal("new ExpressionFramework.Domain.Builders.Expressions.TypedConstantExpressionBuilder{GenericArguments($property.TypeName, true)}()", null)).WithName(MetadataNames.CustomBuilderDefaultValue),
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
            .WithNamespace(base.CurrentNamespace)
            .WithName($"{typeBase.WithoutInterfacePrefix()}Parser")
            .WithBaseClass($"{type}ParserBase")
            .AddConstructors(CreateConstructors(typeBase, name, type))
            .AddMethods(new MethodBuilder()
                .WithName("DoParse")
                .WithReturnTypeName($"{typeof(Result<>).WithoutGenerics()}<{Constants.Namespaces.Domain}.{type}>")
                .AddParameter("context", "CrossCutting.Utilities.Parsers.FunctionCallContext")
                .WithProtected()
                .WithOverride()
                .With(parseMethod => AddParseCodeStatements(typeBase, parseMethod, entityNamespace, type, settings))
            )
            .AddAttributes(CreateAttributes(typeBase, settings, name));

    private IEnumerable<ConstructorBuilder> CreateConstructors(TypeBase typeBase, string name, string type)
    {
        if (type != Constants.Types.Expression)
        {
            yield break;
        }

        var attr = typeBase.Attributes.FirstOrDefault(x => x.Name.GetClassName() == nameof(ExpressionNameAttribute));
        var code = attr is not null
            ? $"base({CsharpExpressionDumper.Dump(attr.Parameters.First().Value)}, {CsharpExpressionDumper.Dump(attr.Parameters.Last().Value)})"
            : $"base({CsharpExpressionDumper.Dump(name)})";

        yield return new ConstructorBuilder().WithChainCall(code);
    }

    private static IEnumerable<AttributeBuilder> CreateAttributes(TypeBase typeBase, PipelineSettings settings, string name)
    {
        yield return new AttributeBuilder().WithName("FunctionName").AddParameters(new AttributeParameterBuilder().WithValue(name));
        foreach (var attribute in typeBase.Properties.Select(x => new AttributeBuilder()
            .WithName("FunctionArgument")
            .AddParameters(CreateParameters(x, settings))))
        {
            yield return attribute;
        }

        switch (typeBase.Interfaces.FirstOrDefault())
        {
            case Constants.TypeNames.Aggregator:
                yield return new AttributeBuilder().WithName("FunctionResultType").AddParameters(new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({Constants.TypeNames.Aggregator})")));
                break;
            case Constants.TypeNames.Expression:
                yield return new AttributeBuilder().WithName("FunctionResultType").AddParameters(new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({Constants.TypeNames.Expression})")));
                break;
            case Constants.TypeNames.Evaluatable:
                yield return new AttributeBuilder().WithName("FunctionResultType").AddParameters(new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({Constants.TypeNames.Evaluatable})")));
                break;
            case Constants.TypeNames.Operator:
                yield return new AttributeBuilder().WithName("FunctionResultType").AddParameters(new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({Constants.TypeNames.Operator})")));
                break;
        }
    }

    private static IEnumerable<AttributeParameterBuilder> CreateParameters(Property property, PipelineSettings settings)
    {
        var typeName = property.TypeName.MapTypeName(settings);
        var isOptional = typeName.EndsWith('?') || property.IsNullable;
        // Fow now, assume System.Object, and let ExpressionFramework do the type checking
        // This is not necessary when you entirely remove the Expression abstraction layer, and just use IFunction and ITypedFunction<T> everywhere...
        typeName = typeof(object).FullName;
        /*typeName = typeName.ReplaceSuffix("?", string.Empty, StringComparison.Ordinal);

        if (typeName.WithoutGenerics() == Constants.TypeNames.TypedExpression)
        {
            typeName = typeName.GetGenericArguments();
        }

        if (typeName == "T")
        {
            // ITypedExpression<T> / T
            // Cannot be determined at compile time, so assume System.Object
            typeName = WellKnownTypes.Object;
        }

        if (typeName == Constants.TypeNames.Expression)
        {
            typeName = WellKnownTypes.Object;
        }

        if (typeName.WithoutGenerics() == typeof(IReadOnlyCollection<>).WithoutGenerics())
        {
            typeName = typeof(IEnumerable).FullName!;
        }

        var genericArguments = typeName.GetGenericArguments();
        if (!string.IsNullOrEmpty(genericArguments))
        {
            // Something<Something, T, Something>
            var newGenericTypeArgs = new List<string>();
            foreach (var genericTypeArg in genericArguments.Split(','))
            {
                newGenericTypeArgs.Add(genericTypeArg switch
                {
                    "T" => WellKnownTypes.Object,
                    Constants.TypeNames.Expression => WellKnownTypes.Object,
                    _ => genericTypeArg
                });
            }
            typeName = typeName.WithoutGenerics().MakeGenericTypeName(string.Join(',', newGenericTypeArgs));
        }*/

        yield return new AttributeParameterBuilder().WithValue(property.Name);
        yield return new AttributeParameterBuilder().WithValue(new StringLiteral($"typeof({typeName})"));

        if (isOptional)
        {
            //required = false
            yield return new AttributeParameterBuilder().WithValue(false);
        }
    }

    protected PipelineSettings CreateSettings()
        => new PipelineSettingsBuilder()
            .AddTypenameMappings(CreateTypenameMappings())
            .AddNamespaceMappings(CreateNamespaceMappings())
            .Build();

    protected static bool IsSupportedPropertyForGeneratedParser(Property parserProperty)
        => parserProperty.TypeName.WithoutGenerics().GetClassName().In($"I{Constants.Types.Expression}", Constants.Types.ITypedExpression)
        || parserProperty.TypeName == $"{Constants.CodeGenerationRootNamespace}.Contracts.{Constants.Types.ITypedExpression}<{typeof(IEnumerable).FullName}>"
        || parserProperty.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.CodeGenerationRootNamespace}.Models.I{Constants.Types.Expression}>";

    private void AddParseCodeStatements(TypeBase typeBase, MethodBuilder parseMethod, string entityNamespace, string type, PipelineSettings settings)
    {
        if (entityNamespace == Constants.Namespaces.DomainExpressions && typeBase.GenericTypeArguments.Count > 0 && typeBase.Properties.Count == 1)
        {
            parseMethod.AddStringCodeStatements($"return ParseTypedExpression(typeof({typeBase.WithoutInterfacePrefix()}<>), 0, {CsharpExpressionDumper.Dump(typeBase.Properties.First().Name)}, context);");
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
            parseMethod.AddStringCodeStatements(typeBase.Properties.Where(x => !IsSupportedPropertyForGeneratedParser(x)).Select(x => $"    {x.Name.ToCamelCase(CultureInfo.InvariantCulture)}Result,"));
            parseMethod.AddStringCodeStatements
            (
                "}.FirstOrDefault(x => !x.IsSuccessful());",
                "if (error is not null)",
                "{",
                $"    return Result.FromExistingResult<{Constants.Namespaces.Domain}.{type}>(error);",
                "}"
            );
        }

        if (typeBase.GenericTypeArguments.Count == 1)
        {
            parseMethod.AddStringCodeStatements
            (
                "var typeResult = context.FunctionCall.Name.GetGenericTypeResult();",
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
        if (property.TypeName.WithoutGenerics().GetClassName() == Constants.Types.ITypedExpression && !string.IsNullOrEmpty(genericType.MethodType))
        {
            return $"var {property.Name.ToCamelCase(CultureInfo.InvariantCulture)}Result = context.GetArgument{genericType.MethodType}ValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix});";
        }
        else if (property.TypeName == typeof(object).FullName || property.TypeName == "object")
        {
            return $"var {property.Name.ToCamelCase(CultureInfo.InvariantCulture)}Result = context.GetArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix});";
        }
        else
        {
            var typeName = property.TypeName.WithoutGenerics() switch
            {
                "System.Nullable" => property.TypeName.GetGenericArguments().MapTypeName(settings),
                "System.Collections.Generic.IReadOnlyCollection" => $"{typeof(IEnumerable<>).WithoutGenerics()}<{property.TypeName.GetGenericArguments().MapTypeName(settings)}>",
                _ => property.TypeName.MapTypeName(settings)
            };
            return $"var {property.Name.ToCamelCase(CultureInfo.InvariantCulture)}Result = context.GetArgumentExpressionResult<{typeName}>({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix});";
        }
    }

    private string CreateParseArguments(TypeBase typeBase, string type)
    {
        var builder = new StringBuilder();
        builder.AppendLine().Append("                ");
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
            builder.AppendLine(",").Append("                ");
        }

        if (property.TypeName.GetClassName() == $"I{Constants.Types.Expression}")
        {
            builder.Append($"new TypedConstantResultExpression<{typeof(object).FullName}{nullableSuffix}>(context.GetArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix}))");
        }
        else if (property.TypeName == $"{typeof(ITypedExpression<>).WithoutGenerics()}<{typeof(IEnumerable).FullName}>")
        {
            builder.Append($"context.GetTypedExpressionsArgumentValueExpression({index}, {CsharpExpressionDumper.Dump(property.Name)})");
        }
        else if (property.TypeName == $"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.CodeGenerationRootNamespace}.Models.I{type}>")
        {
            builder.Append($"context.GetExpressionsArgumentValueResult({index}, {CsharpExpressionDumper.Dump(property.Name)})");
        }
        else if (property.TypeName.WithoutGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            var genericType = GetCustomType(property.TypeName.GetGenericArguments());
            if (!string.IsNullOrEmpty(genericType.MethodType))
            {
                builder.Append($"context.GetArgument{genericType.MethodType}ValueExpression({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix})");
            }
            else
            {
                builder.Append($"context.GetArgumentValueExpression<{property.TypeName.GetGenericArguments()}>({index}, {CsharpExpressionDumper.Dump(property.Name)}{defaultValueSuffix})");
            }
        }
        else
        {
            builder.Append($"{property.Name.ToCamelCase(CultureInfo.InvariantCulture)}Result.Value{nullableBangSuffix}");
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
