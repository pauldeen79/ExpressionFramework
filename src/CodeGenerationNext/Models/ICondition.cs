namespace CodeGenerationNext.Models;

public interface ICondition
{
    IExpression LeftExpression { get; }
    IOperator Operator { get; }
    IExpression RightExpression { get; }
    bool StartGroup { get; }
    bool EndGroup { get; }
    Combination Combination { get; }
}

public enum Combination { }
