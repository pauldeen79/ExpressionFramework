namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ComposedEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    public EvaluatableDescriptor Get()
        => new EvaluatableDescriptor
        (
            name: nameof(ComposedEvaluatable),
            typeName: typeof(ComposedEvaluatable).FullName,
            description: "Evaluates multiple conditions",
            parameters: new[]
            {
                new ParameterDescriptor
                (
                    name: nameof(ComposedEvaluatable.Conditions),
                    typeName: typeof(IReadOnlyCollection<SingleEvaluatable>).FullName,
                    description: "Conditions to evaluate",
                    required: true
                )
            },
            returnValues: new[]
            {
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Ok,
                    value: "true when the conditions evaluate to true, otherwise false",
                    description: "This result will be returned when evaluation of the expressions succeed"
                )
            }
        );
}
