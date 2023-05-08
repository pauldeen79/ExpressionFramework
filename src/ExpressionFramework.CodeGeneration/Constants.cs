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
        public const string DomainSpecialized = "ExpressionFramework.Domain.Specialized";
        public const string Parser = "ExpressionFramework.Parser";

        public const string ParserAggregatorResultParsers = "ExpressionFramework.Parser.AggregatorResultParsers";
        public const string ParserEvaluatableResultParsers = "ExpressionFramework.Parser.EvaluatableResultParsers";
        public const string ParserExpressionResultParsers = "ExpressionFramework.Parser.ExpressionResultParsers";
        public const string ParserOperatorResultParsers = "ExpressionFramework.Parser.OperatorResultParsers";

        public const string DomainBuildersAggregators = "ExpressionFramework.Domain.Builders.Aggregators";
        public const string DomainBuildersEvaluatables = "ExpressionFramework.Domain.Builders.Evaluatables";
        public const string DomainBuildersExpressions = "ExpressionFramework.Domain.Builders.Expressions";
        public const string DomainBuildersOperators = "ExpressionFramework.Domain.Builders.Operators";

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
    }

    [ExcludeFromCodeCoverage]
    public static class TypeNames
    {
        public const string Aggregator = $"{Namespaces.Domain}.Aggregator";
        public const string Evaluatable = $"{Namespaces.Domain}.Evaluatable";
        public const string Expression = $"{Namespaces.Domain}.Expression";
        public const string Operator = $"{Namespaces.Domain}.Operator";

        public static class Expressions
        {
            public const string ConstantExpression = "ConstantExpression";
            public const string TypedConstantExpression = "TypedConstantExpression";
        }
    }

    [ExcludeFromCodeCoverage]
    public static class Paths
    {
        public const string Aggregators = $"{Namespaces.DomainSpecialized}/{nameof(Aggregators)}";
        public const string Evaluatables = $"{Namespaces.DomainSpecialized}/{nameof(Evaluatables)}";
        public const string Expressions = $"{Namespaces.DomainSpecialized}/{nameof(Expressions)}";
        public const string Operators = $"{Namespaces.DomainSpecialized}/{nameof(Operators)}";

        public const string AggregatorBuilders = $"{Namespaces.DomainBuilders}/{nameof(Aggregators)}";
        public const string EvaluatableBuilders = $"{Namespaces.DomainBuilders}/{nameof(Evaluatables)}";
        public const string ExpressionBuilders = $"{Namespaces.DomainBuilders}/{nameof(Expressions)}";
        public const string OperatorBuilders = $"{Namespaces.DomainBuilders}/{nameof(Operators)}";

        public const string ParserAggregatorResultParsers = $"{Namespaces.Parser}/AggregatorResultParsers";
        public const string ParserEvaluatableResultParsers = $"{Namespaces.Parser}/EvaluatableResultParsers";
        public const string ParserExpressionResultParsers = $"{Namespaces.Parser}/ExpressionResultParsers";
        public const string ParserOperatorResultParsers = $"{Namespaces.Parser}/OperatorResultParsers";
        public const string Parser = Namespaces.Parser;
    }

    public static class ArgumentNames
    {
        public const string PredicateExpression = "predicateExpression";
    }
}
