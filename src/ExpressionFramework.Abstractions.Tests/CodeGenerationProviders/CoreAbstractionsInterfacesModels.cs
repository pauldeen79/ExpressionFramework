namespace ExpressionFramework.Abstractions.Tests.CodeGenerationProviders;

public class CoreAbstractionsInterfacesModels : CSharpExpressionDumperClassBase
{
    public override string Path => "ExpressionFramework.CodeGeneration\\CodeGenerationProviders";
    public override string DefaultFileName => "ExpressionFrameworkCSharpClassBase.Core.generated.cs";
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
        typeof(IEmptyExpression),
        typeof(IConstantExpression),
        typeof(IDelegateExpression),
        typeof(IFieldExpression),
    };

    protected override string Namespace => "ExpressionFramework.CodeGeneration.CodeGenerationProviders";
    protected override string ClassName => "ExpressionFrameworkCSharpClassBase";
    protected override string MethodName => "GetCoreModels";
}
