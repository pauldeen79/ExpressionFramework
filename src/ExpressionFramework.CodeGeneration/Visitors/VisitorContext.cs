namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
public class VisitorContext
{
    public Dictionary<string, string> TypedInterfaceMap { get; } = new();
    public Dictionary<string, TypeBaseBuilder> BaseTypes { get; } = new();
}
