namespace ExpressionFramework.Domain.Tests.Unit.Builders.Expressions;

public class StringLengthExpressionBuilderTests
{
    [Fact]
    public void ITypedExpressionBuilder_Build_Returns_Correct_Result()
    {
        // Arrange
        ITypedExpressionBuilder<int> sut = new StringLengthExpressionBuilder().WithExpression(new TypedConstantExpressionBuilder<string>().WithValue("Hello world"));

        // Act
        var result = sut.Build();

        // Assert
        result.ShouldBeOfType<StringLengthExpression>();
    }
}
