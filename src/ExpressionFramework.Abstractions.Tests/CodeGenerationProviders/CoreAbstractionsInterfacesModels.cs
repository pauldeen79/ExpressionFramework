namespace ExpressionFramework.Abstractions.Tests.CodeGenerationProviders;

public class CoreAbstractionsInterfacesModels : CSharpExpressionDumperClassBase
{
    public override string Path => "CodeGeneration/CodeGenerationProviders";
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
        typeof(ICompositeExpression),
        typeof(IItemExpression),
        typeof(IContextExpression),
        typeof(IConditionalExpression),
        typeof(ICompositeFunctionEvaluatorResult),
    };

    protected override string Namespace => "CodeGeneration.CodeGenerationProviders";
    protected override string ClassName => "ExpressionFrameworkCSharpClassBase";
    protected override string MethodName => "GetCoreModels";
}
