﻿namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Evaluatables;

[ExcludeFromCodeCoverage]
public class Entities : ExpressionFrameworkCSharpClassBase
{
    public override string Path => $"{Constants.Namespaces.DomainSpecialized}/{nameof(Evaluatables)}";
    public override string LastGeneratedFilesFileName => string.Empty;

    protected override string FileNameSuffix => string.Empty;
    protected override bool CreateCodeGenerationHeader => false;
    protected override string CurrentNamespace => base.CurrentNamespace.Replace(".Specialized", string.Empty);

    public override object CreateModel()
        => GetOverrideModels(typeof(IEvaluatable))
            .Select(x => new ClassBuilder()
                .WithNamespace(CurrentNamespace)
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithOverride()
                    .AddParameter("context", typeof(object), isNullable: true)
                    .WithType(typeof(Result<bool>))
                    .AddNotImplementedException()
                )
                .Build());
}
