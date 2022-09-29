namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    private readonly EvaluatableDescriptor _descriptor;

    public ReflectionEvaluatableDescriptorProvider(Type type)
    {
        var description = type.GetCustomAttribute<EvaluatableDescriptionAttribute>()?.Description ?? string.Empty;
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
        _descriptor = new EvaluatableDescriptor(
            name: type.Name,
            typeName: type.FullName,
            description,
            parameters,
            returnValues);
    }

    public EvaluatableDescriptor Get() => _descriptor;
}
