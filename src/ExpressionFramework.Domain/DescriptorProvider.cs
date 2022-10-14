namespace ExpressionFramework.Domain;

internal static class DescriptorProvider
{
    internal static string GetDescription<TDescriptionAttribute>(Type type) where TDescriptionAttribute : DescriptionBaseAttribute
        => type.GetCustomAttribute<TDescriptionAttribute>()?.Description ?? string.Empty;

    internal static IEnumerable<ReturnValueDescriptor> GetReturnValues(Type type)
        => type.GetCustomAttributes<ReturnValueAttribute>()
            .Select(x => new ReturnValueDescriptor(x.Status, x.Value, x.ValueType, x.Description));

    internal static IEnumerable<ParameterDescriptor> GetParameters(Type type)
    {
        var parameterDescriptions = type.GetCustomAttributes<ParameterDescriptionAttribute>().ToArray();
        var parameterRequiredIndicators = type.GetCustomAttributes<ParameterRequiredAttribute>().ToArray();
        var parameters = type.GetProperties()
            .Select(x => new ParameterDescriptor(
                x.Name,
                x.PropertyType.FullName,
                parameterDescriptions.FirstOrDefault(y => y.Name == x.Name)?.Description ?? string.Empty,
                parameterRequiredIndicators.FirstOrDefault(y => y.Name == x.Name)?.Required ?? false));
        return parameters;
    }
}
