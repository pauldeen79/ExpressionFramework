namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class SingleEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    public EvaluatableDescriptor Get()
        => new EvaluatableDescriptor
        (
            name: nameof(SingleEvaluatable),
            typeName: typeof(SingleEvaluatable).FullName,
            description: "Evaluates one condition",
            parameters: new[]
            {
                new ParameterDescriptor
                (
                    name: nameof(SingleEvaluatable.LeftExpression),
                    typeName: typeof(Expression).FullName,
                    description: "Left expression",
                    required: true
                ),
                new ParameterDescriptor
                (
                    name: nameof(SingleEvaluatable.Operator),
                    typeName: typeof(Operator).FullName,
                    description: "Operator to use",
                    required: true
                ),
                new ParameterDescriptor
                (
                    name: nameof(SingleEvaluatable.RightExpression),
                    typeName: typeof(Expression).FullName,
                    description: "Right expression",
                    required: true
                )
            },
            returnValues: new[]
            {
                new ReturnValueDescriptor
                (
                    status: ResultStatus.Ok,
                    value: "true when the condition evaluates to true, otherwise false",
                    description: "This result will be returned when evaluation of the expressions succeed"
                )
            }
        );
}
