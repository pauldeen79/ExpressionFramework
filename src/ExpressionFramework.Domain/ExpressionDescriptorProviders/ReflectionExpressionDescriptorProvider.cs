namespace ExpressionFramework.Domain.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProvider : IExpressionDescriptorProvider
{
    private readonly ExpressionDescriptor _descriptor;

    public ReflectionExpressionDescriptorProvider(Type type)
    {
        var description = type.GetCustomAttribute<ExpressionDescriptionAttribute>()?.Description ?? string.Empty;
        var contextTypeName = type.GetCustomAttribute<ExpressionContextTypeAttribute>()?.Type?.FullName;
        var contextDescription = type.GetCustomAttribute<ExpressionContextDescriptionAttribute>()?.Description;
        var contextIsRequired = type.GetCustomAttribute<ExpressionContextRequiredAttribute>()?.Required;
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
        _descriptor = new ExpressionDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            contextTypeName,
            contextDescription,
            contextIsRequired,
            parameters,
            returnValues);
    }

    public ExpressionDescriptor Get() => _descriptor;
}
