namespace ExpressionFramework.Domain.OperatorDescriptorProviders;

public class ReflectionOperatorDescriptorProvider : IOperatorDescriptorProvider
{
    private readonly OperatorDescriptor _descriptor;

    public ReflectionOperatorDescriptorProvider(Type type)
    {
        var description = type.GetCustomAttribute<OperatorDescriptionAttribute>()?.Description ?? string.Empty;
        var usesLeftValue = type.GetCustomAttribute<OperatorUsesLeftValueAttribute>()?.UsesLeftValue ?? false;
        var leftValueTypeName = type.GetCustomAttribute<OperatorLeftValueTypeAttribute>()?.Type?.FullName;
        var usesRightValue = type.GetCustomAttribute<OperatorUsesRightValueAttribute>()?.UsesRightValue ?? false;
        var rightValueTypeName = type.GetCustomAttribute<OperatorRightValueTypeAttribute>()?.Type?.FullName;
        var parameterDescriptions = type.GetCustomAttributes<ParameterDescriptionAttribute>().ToArray();
        var parameterRequiredIndicators = type.GetCustomAttributes<ParameterRequiredAttribute>().ToArray();
        var parameters = type.GetProperties()
            .Select(x => new ParameterDescriptor(
                x.Name,
                x.PropertyType.FullName,
                parameterDescriptions.FirstOrDefault(y => y.Name == x.Name)?.Description ?? string.Empty,
                parameterRequiredIndicators.FirstOrDefault(y => y.Name == x.Name)?.Required ?? false));
        var returnValues = type.GetCustomAttributes<ReturnValueAttribute>()
            .Select(x => new ReturnValueDescriptor(x.Status, x.Value, x.Description));
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
