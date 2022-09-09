namespace ExpressionFramework.Domain.Tests.Support;

public static class OperatorExpression
{
    public static Operator Parse(string @operator)
        => @operator.ToLowerInvariant() switch
        {
            "contains" => new ContainsOperator(),
            "endswith" => new EndsWithOperator(),
            "equals" => new EqualsOperator(),
            "=" => new EqualsOperator(),
            "==" => new EqualsOperator(),
            "greater" => new IsGreaterOperator(),
            "isgreater" => new IsGreaterOperator(),
            ">" => new IsGreaterOperator(),
            "greaterorequal" => new IsGreaterOrEqualOperator(),
            "isgreaterorequal" => new IsGreaterOrEqualOperator(),
            ">=" => new IsGreaterOrEqualOperator(),
            "isnotnull" => new IsNotNullOperator(),
            "notnull" => new IsNotNullOperator(),
            "isnotnullorempty" => new IsNotNullOrEmptyOperator(),
            "notnullorempty" => new IsNotNullOrEmptyOperator(),
            "isnotnullorwhitespace" => new IsNotNullOrWhiteSpaceOperator(),
            "notnullorwhitespace" => new IsNotNullOrWhiteSpaceOperator(),
            "isnull" => new IsNullOperator(),
            "null" => new IsNullOperator(),
            "isnullorempty" => new IsNullOrEmptyOperator(),
            "nullorempty" => new IsNullOrEmptyOperator(),
            "isnullorwhitespace" => new IsNullOrWhiteSpaceOperator(),
            "nullorwhitespace" => new IsNullOrWhiteSpaceOperator(),
            "smaller" => new IsSmallerOperator(),
            "issmaller" => new IsSmallerOperator(),
            "<" => new IsSmallerOperator(),
            "smallerorequal" => new IsSmallerOrEqualOperator(),
            "issmallerorequal" => new IsSmallerOrEqualOperator(),
            "<=" => new IsSmallerOrEqualOperator(),
            "notcontains" => new NotContainsOperator(),
            "notendswith" => new NotEndsWithOperator(),
            "notequals" => new NotEqualsOperator(),
            "notstartswith" => new NotStartsWithOperator(),
            "startswith" => new StartsWithOperator(),
            "unknown" => new UnknownOperator(),
            _ => throw new ArgumentOutOfRangeException($"Unknown operator: [{@operator}]")
        };
}
