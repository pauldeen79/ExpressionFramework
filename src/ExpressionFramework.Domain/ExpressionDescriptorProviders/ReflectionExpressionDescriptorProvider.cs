﻿namespace ExpressionFramework.Domain.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProvider : IExpressionDescriptorProvider
{
    private readonly ExpressionDescriptor _descriptor;

    public ReflectionExpressionDescriptorProvider(Type type)
    {
        var description = DescriptorProvider.GetDescription<ExpressionDescriptionAttribute>(type);
        var usesContext = type.GetCustomAttribute<UsesContextAttribute>()?.UsesContext ?? false;
        var contextTypeName = type.GetCustomAttribute<ContextTypeAttribute>()?.Type.FullName;
        var contextDescription = type.GetCustomAttribute<ContextDescriptionAttribute>()?.Description;
        var contextIsRequired = type.GetCustomAttribute<ContextRequiredAttribute>()?.Required;
        var parameters = DescriptorProvider.GetParameters(type);
        var returnValues = DescriptorProvider.GetReturnValues(type);
        _descriptor = new ExpressionDescriptor(
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

    public ExpressionDescriptor Get() => _descriptor;
}
