namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class OperatorHandlers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/OperatorHandlers";
    public override string DefaultFileName => "OperatorHandlers.cs";
    
    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideOperatorModels()
        .Where(x => IsNotScaffolded(x, "Handler"))
        .Select(x => new ClassBuilder()
            .WithNamespace("ExpressionFramework.Domain.OperatorHandlers")
            .WithName($"{x.Name}Handler")
            .WithBaseClass($"OperatorHandlerBase<{x.Name}>")
            .AddMethods(new ClassMethodBuilder()
                .WithName("Handle")
                .WithProtected()
                .WithOverride()
                .AddParameters(
                    new ParameterBuilder().WithName("leftValue").WithType(typeof(object)).WithIsNullable(),
                    new ParameterBuilder().WithName("rightValue").WithType(typeof(object)).WithIsNullable())
                .WithType(typeof(bool))
                .AddLiteralCodeStatements("throw new NotImplementedException();")
            )
            .Build());
}
