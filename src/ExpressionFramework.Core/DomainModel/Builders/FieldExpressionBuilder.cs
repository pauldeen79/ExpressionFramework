namespace ExpressionFramework.Core.DomainModel.Builders;

public partial class FieldExpressionBuilder
{
    public FieldExpressionBuilder WithFieldName(string fieldName)
        => this.With(x => x.FieldName = fieldName);
}
