namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class FieldExpressionBuilder
{
    public FieldExpressionBuilder(string sourceFieldName) : this(new FieldExpression(sourceFieldName, null))
    {
    }

    public FieldExpressionBuilder WithFieldName(string fieldName)
        => this.With(x => x.FieldName = fieldName);
}
