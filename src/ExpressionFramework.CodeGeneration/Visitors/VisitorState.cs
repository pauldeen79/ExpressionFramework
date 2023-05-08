namespace ExpressionFramework.CodeGeneration.Visitors;

[ExcludeFromCodeCoverage]
internal static class VisitorState
{
    internal static Dictionary<string, string> TypedInterfaceMap { get; } = new();
    internal static Dictionary<string, TypeBaseBuilder> BaseTypes { get; } = new();
}
