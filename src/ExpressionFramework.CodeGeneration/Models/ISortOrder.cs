namespace ExpressionFramework.CodeGeneration.Models;

public interface ISortOrder
{
    IExpression SortExpression { get; }
    SortOrderDirection Direction { get; }
}
