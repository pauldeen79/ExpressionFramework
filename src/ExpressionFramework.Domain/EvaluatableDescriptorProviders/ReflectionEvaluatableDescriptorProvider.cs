namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    private readonly EvaluatableDescriptor _descriptor;

    public ReflectionEvaluatableDescriptorProvider(Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        var description = DescriptorProvider.GetDescription<EvaluatableDescriptionAttribute>(type);
        var usesContext = type.GetCustomAttribute<UsesContextAttribute>()?.UsesContext ?? false;
        var contextTypeName = type.GetCustomAttribute<ContextTypeAttribute>()?.Type.FullName;
        var contextDescription = type.GetCustomAttribute<ContextDescriptionAttribute>()?.Description;
        var contextIsRequired = type.GetCustomAttribute<ContextRequiredAttribute>()?.Required;
        var parameters = DescriptorProvider.GetParameters(type);
        var returnValues = DescriptorProvider.GetReturnValues(type);
        _descriptor = new EvaluatableDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            usesContext,
            contextTypeName,
            contextDescription,
            contextIsRequired,
            parameters,
            returnValues);
    }

    public EvaluatableDescriptor Get() => _descriptor;
}
