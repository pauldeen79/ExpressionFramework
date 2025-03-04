namespace ExpressionFramework.CodeGeneration.Models.Expressions;

[Description("Evaluates a set of cases, and returns the result of the first valid case")]
public interface ISwitchExpression : IExpression
{
    [Required][ValidateObject][Description("Set of cases (scenarios)")] IReadOnlyCollection<ICase> Cases { get; }
    [ValidateObject][Description("Optional expression to use when none of the cases evaluates to false. When left empty, an empty expression will be used.")] IExpression? DefaultExpression { get; }
}
