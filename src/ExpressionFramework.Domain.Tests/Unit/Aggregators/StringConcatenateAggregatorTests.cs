namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class StringConcatenateAggregatorTests
{
    [Fact]
    public void Aggregate_Returns_Invalid_When_Context_Is_Not_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate(null, new ConstantExpression("b"));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of type string");
    }

    [Fact]
    public void Aggregate_Returns_Error_When_SecondExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate("a", new ErrorExpression("Kaboom"));

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_When_SecondExpression_Result_Value_Is_Not_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate("a", new ConstantExpression(1));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Second expression is not of type string");
    }

    [Fact]
    public void Aggregate_Returns_Concatenated_Value_When_Context_And_SecondExpression_Result_Value_Are_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate("a", new ConstantExpression("b"));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("ab");
    }
}
