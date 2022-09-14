namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class Operators : ExpressionFrameworkCSharpClassBase
{
    public override string Path => "ExpressionFramework.Domain/Operators";
    public override string DefaultFileName => "Operators.cs";

    protected override string FileNameSuffix => ".generated";

    public override object CreateModel()
        => GetOverrideOperatorModels()
            .Where(x => IsNotScaffolded(x, string.Empty))
            .Select(x => new ClassBuilder()
                .WithNamespace("ExpressionFramework.Domain.Operators")
                .WithName(x.Name)
                .WithPartial()
                .WithRecord()
                .AddMethods(new ClassMethodBuilder()
                    .WithName("Evaluate")
                    .WithProtected()
                    .WithOverride()
                    .AddParameters
                    (
                        new ParameterBuilder().WithName("leftValue").WithType(typeof(object)).WithIsNullable(),
                        new ParameterBuilder().WithName("rightValueValue").WithType(typeof(object)).WithIsNullable()
                    )
                    .WithTypeName($"{typeof(Result<>).WithoutGenerics()}<{typeof(bool).FullName}>")
                    .AddLiteralCodeStatements("throw new NotImplementedException();")
                )
                .Build());
}
