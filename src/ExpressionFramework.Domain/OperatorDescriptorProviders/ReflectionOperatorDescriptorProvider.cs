namespace ExpressionFramework.Domain.OperatorDescriptorProviders;

public class ReflectionOperatorDescriptorProvider : IOperatorDescriptorProvider
{
    private readonly OperatorDescriptor _descriptor;

    public ReflectionOperatorDescriptorProvider(Type type)
    {
        var description = DescriptorProvider.GetDescription(type);
        var usesLeftValue = type.GetCustomAttribute<OperatorUsesLeftValueAttribute>()?.UsesLeftValue ?? false;
        var leftValueTypeName = type.GetCustomAttribute<OperatorLeftValueTypeAttribute>()?.Type?.FullName;
        var usesRightValue = type.GetCustomAttribute<OperatorUsesRightValueAttribute>()?.UsesRightValue ?? false;
        var rightValueTypeName = type.GetCustomAttribute<OperatorRightValueTypeAttribute>()?.Type?.FullName;
        var parameters = DescriptorProvider.GetParameters(type);
        var returnValues = DescriptorProvider.GetReturnValues(type);
        _descriptor = new OperatorDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            usesLeftValue,
            leftValueTypeName,
            usesRightValue,
            rightValueTypeName,
            parameters,
            returnValues);
    }

    public OperatorDescriptor Get() => _descriptor;
}
