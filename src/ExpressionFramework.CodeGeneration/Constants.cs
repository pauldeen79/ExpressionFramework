namespace ExpressionFramework.CodeGeneration;

public static class Constants
{
    public const string ProjectName = "ExpressionFramework";
    public const string CodeGenerationRootNamespace = $"{ProjectName}.CodeGeneration";

    public static class Namespaces
    {
        public const string Domain = "ExpressionFramework.Domain";
        public const string DomainContracts = "ExpressionFramework.Domain.Contracts";
        public const string DomainBuilders = "ExpressionFramework.Domain.Builders";
        public const string DomainModels = "ExpressionFramework.Domain.Models";
        public const string Parser = "ExpressionFramework.Parser";

        public const string ParserAggregatorResultParsers = "ExpressionFramework.Parser.AggregatorResultParsers";
        public const string ParserEvaluatableResultParsers = "ExpressionFramework.Parser.EvaluatableResultParsers";
        public const string ParserExpressionResultParsers = "ExpressionFramework.Parser.ExpressionResultParsers";
        public const string ParserOperatorResultParsers = "ExpressionFramework.Parser.OperatorResultParsers";

        public const string DomainBuildersAggregators = "ExpressionFramework.Domain.Builders.Aggregators";
        public const string DomainBuildersEvaluatables = "ExpressionFramework.Domain.Builders.Evaluatables";
        public const string DomainBuildersExpressions = "ExpressionFramework.Domain.Builders.Expressions";
        public const string DomainBuildersOperators = "ExpressionFramework.Domain.Builders.Operators";

        public const string DomainModelsAggregators = "ExpressionFramework.Domain.Models.Aggregators";
        public const string DomainModelsEvaluatables = "ExpressionFramework.Domain.Models.Evaluatables";
        public const string DomainModelsExpressions = "ExpressionFramework.Domain.Models.Expressions";
        public const string DomainModelsOperators = "ExpressionFramework.Domain.Models.Operators";

        public const string DomainAggregators = "ExpressionFramework.Domain.Aggregators";
        public const string DomainEvaluatables = "ExpressionFramework.Domain.Evaluatables";
        public const string DomainExpressions = "ExpressionFramework.Domain.Expressions";
        public const string DomainOperators = "ExpressionFramework.Domain.Operators";
    }

    public static class Types
    {
        public const string Aggregator = "Aggregator";
        public const string Evaluatable = "Evaluatable";
        public const string Expression = "Expression";
        public const string ITypedExpression = "ITypedExpression";
        public const string Operator = "Operator";

        public const string AggregatorBuilder = "AggregatorBuilder";
        public const string EvaluatableBuilder = "EvaluatableBuilder";
        public const string ExpressionBuilder = "ExpressionBuilder";
        public const string OperatorBuilder = "OperatorBuilder";

        public const string AggregatorModel = "AggregatorModel";
        public const string EvaluatableModel = "EvaluatableModel";
        public const string ExpressionModel = "ExpressionModel";
        public const string OperatorModel = "OperatorModel";
    }

    [ExcludeFromCodeCoverage]
    public static class TypeNames
    {
        public const string Aggregator = $"{Namespaces.Domain}.Aggregator";
        public const string Evaluatable = $"{Namespaces.Domain}.Evaluatable";
        public const string Expression = $"{Namespaces.Domain}.Expression";
        public const string Operator = $"{Namespaces.Domain}.Operator";
        public const string TypedExpression = $"{Namespaces.Domain}.Contracts.ITypedExpression";

        public static class Expressions
        {
            public const string ConstantExpression = "ConstantExpression";
            public const string DelegateExpression = "DelegateExpression";
            public const string TypedConstantExpression = "TypedConstantExpression";
            public const string TypedDelegateExpression = "TypedDelegateExpression";
        }
    }

    [ExcludeFromCodeCoverage]
    public static class Paths
    {
        public const string Domain = "ExpressionFramework.Domain2";
        public const string DomainBuilders = "ExpressionFramework.Domain2/Builders";

        public const string Aggregators = $"{Domain}/{nameof(Aggregators)}";
        public const string Evaluatables = $"{Domain}/{nameof(Evaluatables)}";
        public const string Expressions = $"{Domain}/{nameof(Expressions)}";
        public const string Operators = $"{Domain}/{nameof(Operators)}";

        public const string AggregatorBuilders = $"{DomainBuilders}/{nameof(Aggregators)}";
        public const string EvaluatableBuilders = $"{DomainBuilders}/{nameof(Evaluatables)}";
        public const string ExpressionBuilders = $"{DomainBuilders}/{nameof(Expressions)}";
        public const string OperatorBuilders = $"{DomainBuilders}/{nameof(Operators)}";

        public const string Parser = "ExpressionFramework.Parser2";
        public const string ParserAggregatorResultParsers = $"{Parser}/AggregatorResultParsers";
        public const string ParserEvaluatableResultParsers = $"{Parser}/EvaluatableResultParsers";
        public const string ParserExpressionResultParsers = $"{Parser}/ExpressionResultParsers";
        public const string ParserOperatorResultParsers = $"{Parser}/OperatorResultParsers";
    }

    public static class ArgumentNames
    {
        public const string PredicateExpression = "predicateExpression";
    }
}
