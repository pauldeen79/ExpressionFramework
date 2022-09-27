namespace ExpressionFramework.Domain.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProvider : IEvaluatableDescriptorProvider
{
    private readonly EvaluatableDescriptor _descriptor;

    public ReflectionEvaluatableDescriptorProvider(Type type)
    {
        var description = type.GetCustomAttribute<EvaluatableDescriptionAttribute>()?.Description ?? string.Empty;
        var parameterDescriptions = type.GetCustomAttributes<EvaluatableParameterDescriptionAttribute>().Select(x => new { x.Name, x.Description }).ToArray();
        var parameterRequiredIndicators = type.GetCustomAttributes<EvaluatableParameterRequiredAttribute>().Select(x => new { x.Name, x.Required }).ToArray();
        var parameters = type.GetProperties()
            .Select(x => new ParameterDescriptor(
                x.Name,
                x.PropertyType.FullName,
                parameterDescriptions.FirstOrDefault(y => y.Name == x.Name)?.Description ?? string.Empty,
                parameterRequiredIndicators.FirstOrDefault(y => y.Name == x.Name)?.Required ?? false));
        var returnValues = type.GetCustomAttributes<EvaluatableReturnValueAttribute>().Select(x =>
            new ReturnValueDescriptor(x.Status, x.Value, x.Description));
        _descriptor = new EvaluatableDescriptor(type.Name, type.FullName, description, parameters, returnValues);
    }

    public EvaluatableDescriptor Get() => _descriptor;
}
