namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders;

[ExcludeFromCodeCoverage]
public class EvaluatableParsers : ExpressionFrameworkCSharpClassBase
{
    public override string Path => Constants.Paths.ParserEvaluatableResultParsers;

    public override object CreateModel()
        => GetOverrideModels(typeof(IEvaluatable))
            .Select(x => CreateParserClass
            (
                x,
                Constants.Types.Evaluatable,
                x.Name,
                Constants.Namespaces.DomainEvaluatables
            ).Build());
}
