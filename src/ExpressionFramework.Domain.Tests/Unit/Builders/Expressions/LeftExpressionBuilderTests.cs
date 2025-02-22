namespace ExpressionFramework.Domain.Tests.Unit.Builders.Expressions;

public class LeftExpressionBuilderTests
{
    [Fact]
    public void ITypedExpressionBuilder_Build_Returns_Correct_Result()
    {
        // Arrange
        ITypedExpressionBuilder<string> sut = new LeftExpressionBuilder().WithExpression(new TypedConstantExpressionBuilder<string>().WithValue("Hello world"));

        // Act
        var result = sut.Build();

        // Assert
        result.ShouldBeOfType<LeftExpression>();
    }
}
