namespace ExpressionFramework.Domain;

public static class StringExpression
{
    public static ExpressionDescriptor GetStringEdgeDescriptor(
        Type type,
        string description,
        string expressionDescription,
        string okValue,
        string okDescription)
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            null,
            null,
            null,
            new[]
            {
                new ParameterDescriptor("LengthExpression", typeof(int).FullName, "Number of characters to use", true),
                new ParameterDescriptor("Expression", typeof(string).FullName, expressionDescription, true),
            },
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(string), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", null, "Expression must be of type string, LengthExpression did not return an integer, Length must refer to a location within the string"),
            }
        );

    public static ExpressionDescriptor GetStringTrimDescriptor(
        Type type,
        string description,
        string expressionDescription,
        string okValue,
        string okDescription)
        => new(
            type.Name,
            type.FullName,
            description,
            true,
            null,
            null,
            null,
            new[]
            {
                new ParameterDescriptor("TrimCharsExpression", typeof(int).FullName, "Optional trim characters to use. When empty, space will be used", true),
                new ParameterDescriptor("Expression", typeof(string).FullName, expressionDescription, true),
            },
            new[]
            {
                new ReturnValueDescriptor(ResultStatus.Ok, okValue, typeof(string), okDescription),
                new ReturnValueDescriptor(ResultStatus.Invalid, "Empty", null, "Expression must be of type string, TrimCharsExpression must be of type char[]"),
            }
        );
}
