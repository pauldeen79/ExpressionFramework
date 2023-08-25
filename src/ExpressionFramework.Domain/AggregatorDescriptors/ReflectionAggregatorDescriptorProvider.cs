namespace ExpressionFramework.Domain.AggregatorDescriptors;

public class ReflectionAggregatorDescriptorProvider : IAggregatorDescriptorProvider
{
    private readonly AggregatorDescriptor _descriptor;

    public ReflectionAggregatorDescriptorProvider(Type type)
    {
        if (type is null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        var description = DescriptorProvider.GetDescription<AggregatorDescriptionAttribute>(type);
        var contextTypeName = type.GetCustomAttribute<ContextTypeAttribute>()?.Type.FullName ?? string.Empty;
        var contextDescription = type.GetCustomAttribute<ContextDescriptionAttribute>()?.Description ?? string.Empty;
        var parameters = DescriptorProvider.GetParameters(type);
        var returnValues = DescriptorProvider.GetReturnValues(type);
        _descriptor = new AggregatorDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            contextTypeName,
            contextDescription,
            parameters,
            returnValues);
    }

    public AggregatorDescriptor Get()
        => _descriptor;
}
