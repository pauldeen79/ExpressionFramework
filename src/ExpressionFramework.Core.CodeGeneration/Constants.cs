namespace ExpressionFramework.CodeGeneration;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ProjectName = "ExpressionFramework";
    public const string CodeGenerationRootNamespace = $"{ProjectName}.CodeGeneration";

    public static class Namespaces
    {
        public const string Core = "ExpressionFramework.Core";
        public const string CoreBuilders = "ExpressionFramework.Core.Builders";

        public const string CoreEvaluatables = "ExpressionFramework.Core.Evaluatables";
        public const string CoreOperators = "ExpressionFramework.Core.Operators";
    }

    public static class Paths
    {
        public const string Core = "ExpressionFramework.Core";
        public const string CoreBuilders = "ExpressionFramework.Core/Builders";

        public const string Evaluatables = $"{Core}/{nameof(Evaluatables)}";
        public const string Operators = $"{Core}/{nameof(Operators)}";

        public const string EvaluatableBuilders = $"{CoreBuilders}/{nameof(Evaluatables)}";
        public const string OperatorBuilders = $"{CoreBuilders}/{nameof(Operators)}";
    }
}
