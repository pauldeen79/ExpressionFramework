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
            .Select(x => new
            {
                Source = x,
                Target = new ClassBuilder()
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
            })
            .Select(x =>
            {
                var typedInterface = x.Source.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics())?.FixTypeName();
                if (!string.IsNullOrEmpty(typedInterface))
                {
                    x.Target.AddMethods(new ClassMethodBuilder()
                            .WithName("EvaluateTyped")
                            .AddParameters(new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable())
                            .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typedInterface.GetGenericArguments()}>")
                            .AddNotImplementedException()
                    );
                }

                return x.Target
                    .AddGenericTypeArguments(x.Source.GenericTypeArguments)
                    .AddGenericTypeArgumentConstraints(x.Source.GenericTypeArgumentConstraints)
                    .Build();
            });
}
