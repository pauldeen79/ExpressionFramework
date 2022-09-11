namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class ExpressionHandlers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/ExpressionHandlers";
    public override string DefaultFileName => "ExpressionHandlers.cs";

    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideExpressionModels()
        .Where(x => IsNotScaffolded(x, "Handler"))
        .Select(x => new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.ExpressionHandlers")
            .WithName($"{x.Name}Handler")
            .WithBaseClass($"ExpressionHandlerBase<{x.Name}>")
            .AddMethods(new ClassMethodBuilder()
                .WithName("Handle")
                .WithProtected()
                .WithOverride()
                .AddParameters(
                    new ParameterBuilder().WithName("context").WithType(typeof(object)).WithIsNullable(),
                    new ParameterBuilder().WithName("typedExpression").WithTypeName(x.Name),
                    new ParameterBuilder().WithName("evaluator").WithTypeName($"ExpressionFramework.Domain.{nameof(IExpressionEvaluator)}"))
                .WithTypeName("Task<Result<object?>>")
                .AddLiteralCodeStatements("throw new NotImplementedException();")
            )
            .Build());
}
