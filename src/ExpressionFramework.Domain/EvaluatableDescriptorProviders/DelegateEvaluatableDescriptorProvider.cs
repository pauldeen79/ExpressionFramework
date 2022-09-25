namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class DelegateEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    public EvaluatableDescriptor Get()
        => new EvaluatableDescriptor
        (
            name: nameof(DelegateEvaluatable),
            typeName: typeof(DelegateEvaluatable).FullName,
            description: "Returns the delegate value provided",
            parameters: new[]
            {
                new ParameterDescriptor
                (
                    name: nameof(DelegateEvaluatable.Value),
                    typeName: typeof(Func<bool>).FullName,
                    description: "Value to use",
                    required: true
                )
            },
            returnValues: new[]
            {
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Ok,
                    value: "The value that is returned from the supplied delegate using the Value parameter",
                    description: "This result will always be returned"
                )
            }
        );
}
