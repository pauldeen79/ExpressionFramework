namespace ExpressionFramework.Domain.ExpressionDescriptorProviders;

public class ChainedExpressionDescriptorProvider : IExpressionDescriptorProvider
{
    public ExpressionDescriptor Get()
        => new ExpressionDescriptor
        (
            name: nameof(ChainedExpression),
            typeName: typeof(ChainedExpression).FullName,
            description: "Chains the result of an expression onto the next one, and so on",
            contextTypeName: typeof(object).FullName,
            contextIsRequired: false,
            parameters: new[]
            {
                new ParameterDescriptor
                (
                    name: nameof(ChainedExpression.Expressions),
                    typeName: typeof(IReadOnlyCollection<Expression>).FullName,
                    description: "Expressions to use on chaining. The context is chained to the first expression.",
                    required: true
                )
            },
            returnValues: new[]
            {
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Ok,
                    value: "Result value of the last expression",
                    description: "This will be returned in case the last expression returns success (Ok)"
                ),
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Error,
                    value: "Empty",
                    description: "This status (or any other status not equal to Ok) will be returned in case any expression returns something else than Ok, in which case subsequent expressions will not be executed anymore"
                )
            }
        );
}
