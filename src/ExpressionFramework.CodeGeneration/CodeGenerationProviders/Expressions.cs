namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Expressions : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecialized}/{nameof(Expressions)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specialized", string.Empty);

    public override object CreateModel()
        => GetOverrideModels(typeof(IExpression))
            .Select(x =>
            {
                var result = new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(object).FullName}?>")
                    .AddNotImplementedException()
                );
            
                var typedInterface = x.Interfaces.FirstOrDefault(x => x != null && x.WithoutProcessedGenerics() == typeof(ITypedExpression<>).WithoutGenerics())?.FixTypeName();
                if (!string.IsNullOrEmpty(typedInterface))
                {
                    result
                        .AddMethods(new ClassMethodBuilder()
                        .WithName("EvaluateTyped")
                        .AddParameter("context", typeof(object), isNullable: true)
                        .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typedInterface.GetGenericArguments()}>")
                        .AddNotImplementedException()
                    );
                }

                return result
                    .AddGenericTypeArguments(x.GenericTypeArguments)
                    .AddGenericTypeArgumentConstraints(x.GenericTypeArgumentConstraints)
                    .Build();
            });
}
