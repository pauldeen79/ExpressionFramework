namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToLowerCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LowerCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate("LOWER");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("lower");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_Wnen_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Null_Wnen_Context_Is_Null()
    {
        // Arrange
        var sut = new ToLowerCaseExpression();

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.GetValueOrThrow().Should().BeNull();
    }
}
