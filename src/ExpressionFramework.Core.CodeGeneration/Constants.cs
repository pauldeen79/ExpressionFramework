namespace ExpressionFramework.CodeGeneration;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public const string ProjectName = "ExpressionFramework";
    public const string CodeGenerationRootNamespace = $"{ProjectName}.CodeGeneration";

    public static class Namespaces
    {
        public const string Core = "ExpressionFramework.Core";
        //public const string DomainContracts = "ExpressionFramework.Core.Contracts";
        public const string CoreBuilders = "ExpressionFramework.Core.Builders";
        //public const string DomainModels = "ExpressionFramework.Domain.Models";

        //public const string DomainBuildersEvaluatables = "ExpressionFramework.Domain.Builders.Evaluatables";
        //public const string DomainBuildersOperators = "ExpressionFramework.Domain.Builders.Operators";

        //public const string DomainModelsEvaluatables = "ExpressionFramework.Domain.Models.Evaluatables";
        //public const string DomainModelsOperators = "ExpressionFramework.Domain.Models.Operators";

        public const string CoreEvaluatables = "ExpressionFramework.Core.Evaluatables";
        public const string DomainOperators = "ExpressionFramework.Domain.Operators";
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
