namespace ExpressionFramework.Abstractions.Tests.CodeGenerationProviders;

public class BaseAbstractionsInterfacesModels : CSharpExpressionDumperClassBase
{
    public override string Path => "CodeGeneration/CodeGenerationProviders";
    public override string DefaultFileName => "ExpressionFrameworkCSharpClassBase.Base.generated.cs";
    public override bool RecurseOnDeleteGeneratedFiles => false;

    protected override string[] NamespacesToAbbreviate => new[]
    {
        "System.Collections.Generic",
        "ModelFramework.Objects.Builders",
        "ModelFramework.Objects.Contracts"
    };

    protected override Type[] Models => new[]
    {
        typeof(ICondition),
        typeof(IExpression),
        typeof(IExpressionFunction)
    };

    protected override string Namespace => "CodeGeneration.CodeGenerationProviders";
    protected override string ClassName => "ExpressionFrameworkCSharpClassBase";
    protected override string MethodName => "GetBaseModels";
}
