namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Expressions : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.Domain}/Expressions";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddNotImplementedException()
                )
                .AddGenericTypeArguments(x.GenericTypeArguments)
                .AddGenericTypeArgumentConstraints(x.GenericTypeArgumentConstraints)
                .Build());
}
