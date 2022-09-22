namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EmptyExpressionTests
{
    [Fact]
    public void ValidateWithContext_Return_Empty_Sequence()
    {
        // Arrange
        var sut = new EmptyExpression();

        // Act
        var actual = sut.ValidateContext(null, new ValidationContext(sut));

        // Assert
        actual.Should().BeEmpty();
    }
}
