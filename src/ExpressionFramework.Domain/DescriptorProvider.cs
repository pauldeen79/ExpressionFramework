namespace ExpressionFramework.Domain;

internal static class DescriptorProvider
{
    internal static string GetDescription<TDescriptionAttribute>(Type type) where TDescriptionAttribute : DescriptionBaseAttribute
        => type.GetCustomAttribute<TDescriptionAttribute>()?.Description ?? string.Empty;

    internal static IEnumerable<ReturnValueDescriptor> GetReturnValues(Type type)
        => type
            .GetCustomAttributes<ReturnValueAttribute>()
            .Select(x => new ReturnValueDescriptor(x.Status, x.Value, x.ValueType, x.Description));

    internal static IEnumerable<ParameterDescriptor> GetParameters(Type type)
    {
        var parameterTypes = type.GetCustomAttributes<ParameterTypeAttribute>().ToArray();
        var parameterDescriptions = type.GetCustomAttributes<ParameterDescriptionAttribute>().ToArray();
        var parameterRequiredIndicators = type.GetCustomAttributes<ParameterRequiredAttribute>().ToArray();
        var parameters = type.GetProperties()
            .Select(x => new ParameterDescriptor(
                x.Name,
                Array.Find(parameterTypes, y => y.Name == x.Name)?.Type.FullName ?? x.PropertyType.FullName,
                Array.Find(parameterDescriptions, y => y.Name == x.Name)?.Description ?? string.Empty,
                Array.Find(parameterRequiredIndicators, y => y.Name == x.Name)?.Required ?? false));

        return parameters;
    }
}
