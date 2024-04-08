namespace ExpressionFramework.CodeGeneration.BuilderComponents;

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderComponentBuilder : IBuilderComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public TypedExpressionBuilderComponentBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public IPipelineComponent<IConcreteTypeBuilder, BuilderContext> Build()
        => new TypedExpressionBuilderComponent(_formattableStringParser);
}

[ExcludeFromCodeCoverage]
public class TypedExpressionBuilderComponent : IPipelineComponent<IConcreteTypeBuilder, BuilderContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public TypedExpressionBuilderComponent(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Result<IConcreteTypeBuilder> Process(PipelineContext<IConcreteTypeBuilder, BuilderContext> context)
    {
        context = context.IsNotNull(nameof(context));

        foreach (var property in context.Context.GetSourceProperties().Where(context.Context.IsValidForFluentMethod))
        {
            var parentChildContext = new ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property>(context, property, context.Context.Settings);

            if (!property.TypeName.FixTypeName().IsCollectionTypeName() && property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
            {
                var results = context.Context.GetResultsForBuilderNonCollectionProperties(property, parentChildContext, _formattableStringParser);

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Result.FromExistingResult<IConcreteTypeBuilder>(error.Result);
                }

                AddOverloadsForTypedExpression(context, property, results);
            }
            else if (property.TypeName.StartsWith($"{typeof(IReadOnlyCollection<>).WithoutGenerics()}<{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}"))
            {
                var results = context.Context.GetResultsForBuilderCollectionProperties
                (
                    property,
                    parentChildContext,
                    _formattableStringParser,
                    GetCodeStatementsForEnumerableOverload(context, property, parentChildContext),
                    GetCodeStatementsForArrayOverload(context, property, parentChildContext)
                );

                var error = Array.Find(results, x => !x.Result.IsSuccessful());
                if (error is not null)
                {
                    // Error in formattable string parsing
                    return Result.FromExistingResult<IConcreteTypeBuilder>(error.Result);
                }

                AddOverloadsForTypedExpressions(context, property, results);
            }
        }

        return Result.Continue<IConcreteTypeBuilder>();
    }

    private static void AddOverloadsForTypedExpression(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, NamedResult<Result<string>>[] results)
    {
        // Add builder overload that uses T instead of ITypedExpression<T>, and calls the other overload.
        // we need the Value propery of Nullable<T> for value types...
        // for now, we only support int, long, decimal and boolean
        var isValueType = property.TypeName.GetGenericArguments().In("System.Int32", "System.Int64", "System.Boolean", "System.Decimal", "System.DateTime", "int", "long", "bool", "decimal");
        //TODO: Find a good solution in ClassFramework for checking on value types. For this, we have to add a property GenericTypeArguments to ITypeContainer, which is an IReadOnlyCollection of ITypeContainer instances...
        var suffix = property.IsNullable && isValueType
            ? ".Value"
            : string.Empty;

        var builder = new MethodBuilder()
            .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
            .WithReturnTypeName(context.Context.IsBuilderForAbstractEntity
                ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
                : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithTypeName(CreateTypeName(property))
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Context.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        //if (context.Context.Settings.AddNullChecks && !isValueType && property.TypeName != "T") //note that we need a hack here, because ArgumentNullCheck doesn't understnad ITypedExpression<bla> where bla is either value or reference type... again, this could be solved by proper support for generics, including a valuetype indicator
        //{
        //    var nullCheckStatement = results.First(x => x.Name == "ArgumentNullCheck").Result.Value!;
        //    if (!string.IsNullOrEmpty(nullCheckStatement))
        //    {
        //        builder.AddStringCodeStatements(nullCheckStatement);
        //    }
        //}

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()}{suffix});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()}{suffix});",
            context.Context.ReturnValueStatementForFluentMethod
        );

        context.Model.AddMethods(builder);

        builder = new MethodBuilder()
            .WithName(results.First(x => x.Name == "MethodName").Result.Value!)
            .WithReturnTypeName(context.Context.IsBuilderForAbstractEntity
                  ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
                  : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}")
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(Func<>).WithoutGenerics()}<{typeof(object).FullName}?, {CreateTypeName(property)}>")
                    .WithIsNullable(property.IsNullable)
                    .WithDefaultValue(context.Context.GetMappingMetadata(property.TypeName).GetValue<object?>(MetadataNames.CustomBuilderWithDefaultPropertyValue, () => null))
            );

        //if (context.Context.Settings.AddNullChecks && !isValueType && property.TypeName != "T") //note that we need a hack here, because ArgumentNullCheck doesn't understnad ITypedExpression<bla> where bla is either value or reference type... again, this could be solved by proper support for generics, including a valuetype indicator
        //{
        //    var nullCheckStatement = results.First(x => x.Name == "ArgumentNullCheck").Result.Value!;
        //    if (!string.IsNullOrEmpty(nullCheckStatement))
        //    {
        //        builder.AddStringCodeStatements(nullCheckStatement);
        //    }
        //}

        builder.AddStringCodeStatements
        (
            property.IsNullable
                ? $"{property.Name} = {property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()} is null ? null : new {Constants.TypeNames.Expressions.TypedDelegateExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});"
                : $"{property.Name} = new {Constants.TypeNames.Expressions.TypedDelegateExpression}Builder<{property.TypeName.GetGenericArguments()}>().WithValue({property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()});",
            context.Context.ReturnValueStatementForFluentMethod
        );

        context.Model.AddMethods(builder);
    }

    private static void AddOverloadsForTypedExpressions(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, NamedResult<Result<string>>[] results)
    {
        var returnType = context.Context.IsBuilderForAbstractEntity
            ? $"TBuilder{context.Context.SourceModel.GetGenericTypeArgumentsString()}"
            : $"{results.First(x => x.Name == "Namespace").Result.Value.AppendWhenNotNullOrEmpty(".")}{results.First(x => x.Name == "BuilderName").Result.Value}{context.Context.SourceModel.GetGenericTypeArgumentsString()}";

        context.Model.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{CreateTypeName(property)}>")
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "EnumerableOverload").Select(x => x.Result.Value!))
        );

        context.Model.AddMethods(new MethodBuilder()
            .WithName(results.First(x => x.Name == "AddMethodName").Result.Value!)
            .WithReturnTypeName(returnType)
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()))
                    .WithTypeName($"{typeof(IEnumerable<>).WithoutGenerics()}<{CreateTypeName(property)}>".ConvertTypeNameToArray())
                    .WithIsParamArray()
            )
            .AddStringCodeStatements(results.Where(x => x.Name == "ArrayOverload").Select(x => x.Result.Value!))
        );
    }

    private IEnumerable<Result<string>> GetCodeStatementsForEnumerableOverload(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property> parentChildContext)
    {
        if (context.Context.Settings.BuilderNewCollectionTypeName == typeof(IEnumerable<>).WithoutGenerics())
        {
            // When using IEnumerable<>, do not call ToArray because we want lazy evaluation
            foreach (var statement in GetCodeStatementsForArrayOverload(context, property, parentChildContext))
            {
                yield return statement;
            }

            yield break;
        }

        // When not using IEnumerable<>, we can simply force ToArray because it's stored in a generic list or collection of some sort anyway.
        // (in other words, materialization is always performed)
        if (context.Context.Settings.AddNullChecks)
        {
            yield return Result.Success(context.Context.CreateArgumentNullException(property.Name.ToPascalCase(context.Context.FormatProvider.ToCultureInfo()).GetCsharpFriendlyName()));
        }

        yield return _formattableStringParser.Parse("return {BuilderAddMethodName}({NamePascalCsharpFriendlyName}.ToArray());", context.Context.FormatProvider, parentChildContext);
    }

    private IEnumerable<Result<string>> GetCodeStatementsForArrayOverload(PipelineContext<IConcreteTypeBuilder, BuilderContext> context, Property property, ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property> parentChildContext)
    {
        if (context.Context.Settings.AddNullChecks)
        {
            var argumentNullCheckResult = _formattableStringParser.Parse
            (
                context.Context.GetMappingMetadata(property.TypeName).GetStringValue(MetadataNames.CustomBuilderArgumentNullCheckExpression, "{NullCheck.Argument}"),
                context.Context.FormatProvider,
                new ParentChildContext<PipelineContext<IConcreteTypeBuilder, BuilderContext>, Property>(context, property, context.Context.Settings)
            );

            if (!string.IsNullOrEmpty(argumentNullCheckResult.Value) || !argumentNullCheckResult.IsSuccessful())
            {
                yield return argumentNullCheckResult;
            }
        }
        
        yield return _formattableStringParser.Parse($"return {{BuilderAddMethodName}}({{NamePascalCsharpFriendlyName}}.Select(x => new {Constants.Namespaces.DomainBuildersExpressions}.{Constants.TypeNames.Expressions.TypedConstantExpression}Builder<{CreateTypeName(property)}>().WithValue(x)));", context.Context.FormatProvider, parentChildContext);
    }

    private static string CreateTypeName(Property property)
    {
        if (property.TypeName.WithoutProcessedGenerics().GetClassName() == Constants.Types.ITypedExpression)
        {
            if (property.Name == Constants.ArgumentNames.PredicateExpression)
            {
                // hacking here... we only want to allow to inject the typed expression
                return property.TypeName;
            }
            else
            {
                return property.TypeName.GetGenericArguments();
            }
        }

        if (property.TypeName.GetGenericArguments().StartsWith($"{Constants.Namespaces.DomainContracts}.{Constants.Types.ITypedExpression}"))
        {
            return property.TypeName.GetGenericArguments().GetGenericArguments();
        }

        if (property.TypeName.GetClassName() == Constants.Types.Expression)
        {
            return typeof(object).FullName!;
        }

        return property.TypeName;
    }
}
