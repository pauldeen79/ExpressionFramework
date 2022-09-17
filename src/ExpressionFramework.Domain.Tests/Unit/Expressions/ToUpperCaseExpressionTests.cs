namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ToUpperCaseExpressionTests
{
    [Fact]
    public void Evaluate_Returns_UpperCase_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate("Upper");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("UPPER");
    }

    [Fact]
    public void Evaluate_Returns_EmptyString_Wnen_Context_Is_EmptyString()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo(string.Empty);
    }

    [Fact]
    public void Evaluate_Returns_Null_Wnen_Context_Is_Null()
    {
        // Arrange
        var sut = new ToUpperCaseExpression();

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.GetValueOrThrow().Should().BeNull();
    }
}
