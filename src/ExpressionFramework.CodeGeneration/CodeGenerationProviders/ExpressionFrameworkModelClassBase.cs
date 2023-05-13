namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

public abstract class ExpressionFrameworkModelClassBase : ExpressionFrameworkCSharpClassBase
{
    protected override string AddMethodNameFormatString => string.Empty; // we don't want Add methods for collection properties
    protected override string SetMethodNameFormatString => string.Empty; // we don't want With methods for non-collection properties
    protected override string BuilderNameFormatString => "{0}Model";
    protected override string BuilderBuildMethodName => "ToEntity";
    protected override string BuilderBuildTypedMethodName => "ToTypedEntity";
    protected override string BuilderName => "Model";
    protected override string BuildersName => "Models";
    protected override string BuilderFactoryName => "ModelFactory";
    protected override bool UseLazyInitialization => false; // we don't want lazy stuff in models, just getters and setters
    protected override bool ConvertStringToStringBuilderOnBuilders => false;
}
