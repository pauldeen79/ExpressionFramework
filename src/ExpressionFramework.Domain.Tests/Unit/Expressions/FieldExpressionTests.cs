namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FieldExpressionTests
{
    [Fact]
    public void Should_Throw_On_Construction_With_Empty_FieldName()
    {
        // Act
        this.Invoking(_ => new FieldExpression(string.Empty))
            .Should().Throw<ValidationException>()
            .WithMessage("The FieldName field is required.");
    }
}
