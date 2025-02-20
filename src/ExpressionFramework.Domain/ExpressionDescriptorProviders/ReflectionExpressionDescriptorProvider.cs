﻿namespace ExpressionFramework.Domain.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProvider : IExpressionDescriptorProvider
{
    private readonly ExpressionDescriptor _descriptor;

    public ReflectionExpressionDescriptorProvider(Type type)
    {
        type = ArgumentGuard.IsNotNull(type, nameof(type));

        var dynamicDesciptorProvider = type.GetCustomAttribute<DynamicDescriptorAttribute>()?.Type;
        if (dynamicDesciptorProvider is not null)
        {
            _descriptor = (ExpressionDescriptor)type.GetMethod("GetExpressionDescriptor", BindingFlags.Public | BindingFlags.Static).Invoke(null, Array.Empty<object>());
        }
        else
        {
            var description = DescriptorProvider.GetDescription<ExpressionDescriptionAttribute>(type);
            var usesContext = type.GetCustomAttribute<UsesContextAttribute>()?.UsesContext ?? false;
            var contextTypeName = type.GetCustomAttribute<ContextTypeAttribute>()?.Type.FullName;
            var contextDescription = type.GetCustomAttribute<ContextDescriptionAttribute>()?.Description;
            var contextIsRequired = type.GetCustomAttribute<ContextRequiredAttribute>()?.Required;
            var parameters = DescriptorProvider.GetParameters(type);
            var returnValues = DescriptorProvider.GetReturnValues(type);
            _descriptor = new ExpressionDescriptor(
                name: RemoveGenerics(type.Name),
                typeName: type.FullName,
                description,
                usesContext,
                contextTypeName,
                contextDescription,
                contextIsRequired,
                parameters,
                returnValues);
        }
    }

    public ExpressionDescriptor Get() => _descriptor;

    private static string RemoveGenerics(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return name;
        }

        var index = name.IndexOf("`");
        if (index == -1)
        {
            return name;
        }

        return name.Substring(0, index);
    }
}
