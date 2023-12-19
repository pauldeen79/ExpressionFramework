namespace ExpressionFramework.CodeGeneration.CodeGenerationProviders.Operators;

[ExcludeFromCodeCoverage]
public class OperatorModelFactory : ExpressionFrameworkModelClassBase
{
    public override string Path => Constants.Namespaces.DomainModels;

    public override object CreateModel()
        => CreateBuilderFactories(
            GetOverrideModels(typeof(IOperator)),
            new(
                Constants.Namespaces.DomainModels,
                nameof(OperatorModelFactory),
                Constants.TypeNames.Operator,
                Constants.Namespaces.DomainModelsOperators,
                Constants.Types.OperatorModel,
                Constants.Namespaces.DomainOperators
            )
        );
}
