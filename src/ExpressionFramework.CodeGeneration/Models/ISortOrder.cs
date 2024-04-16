namespace ExpressionFramework.CodeGeneration.Models;

public interface ISortOrder
{
    [ValidateObject] IExpression SortExpression { get; }
    SortOrderDirection Direction { get; }
}
