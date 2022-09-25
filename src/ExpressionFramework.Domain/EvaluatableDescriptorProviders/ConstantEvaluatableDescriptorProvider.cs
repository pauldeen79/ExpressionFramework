namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ConstantEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    public EvaluatableDescriptor Get()
        => new EvaluatableDescriptor
        (
            name: nameof(ConstantEvaluatable),
            typeName: typeof(ConstantEvaluatable).FullName,
            description: "Returns the constant value provided",
            parameters: new[]
            {
                new ParameterDescriptor
                (
                    name: nameof(ConstantEvaluatable.Value),
                    typeName: typeof(bool).FullName,
                    description: "Value to use",
                    required: true
                )
            },
            returnValues: new[]
            {
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Ok,
                    value: "The value that is supplied with the Value parameter",
                    description: "This result will always be returned"
                )
            }
        );
}
