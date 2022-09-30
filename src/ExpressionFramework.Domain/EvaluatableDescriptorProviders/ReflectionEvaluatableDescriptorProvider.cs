namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    private readonly EvaluatableDescriptor _descriptor;

    public ReflectionEvaluatableDescriptorProvider(Type type)
    {
        var description = DescriptorProvider.GetDescription<EvaluatableDescriptionAttribute>(type);
        var parameters = DescriptorProvider.GetParameters(type);
        var returnValues = DescriptorProvider.GetReturnValues(type);
        _descriptor = new EvaluatableDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            parameters,
            returnValues);
    }

    public EvaluatableDescriptor Get() => _descriptor;
}
