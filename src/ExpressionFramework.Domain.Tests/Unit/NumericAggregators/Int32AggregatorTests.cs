namespace ExpressionFramework.Domain.Tests.Unit.NumericAggregators;

public class Int32AggregatorTests
{
    [Fact]
    public void Aggregate_Return_NotSupported_When_FirstExpression_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new Int32Aggregator();
        int value = 2;

        // Act
        var result = sut.Aggregate(null, new ConstantExpression("unsupported type"), new ConstantExpression(value), (_, _) => "some value");

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Aggregate_Returns_Error_When_FirstExpression_Returns_Error()
    {
        // Arrange
        var sut = new Int32Aggregator();
        int value = 2;

        // Act
        var result = sut.Aggregate(null, new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), new ConstantExpression(value), (_, _) => "some value");

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_When_SecondValue_Could_Not_Be_Converted_To_Correct_Type()
    {
        // Arrange
        var sut = new Int32Aggregator();
        int value = 2;

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(value), new ConstantExpression("unsupported type"), (_, _) => "some value");

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could not convert SecondExpression to Int32. Error message: The input string 'unsupported type' was not in a correct format.");
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Aggregate_Returns_Success_With_Correct_Value_When_FirstExpression_Is_Of_Correct_Type_And_SecondExpression_Can_Be_Converted_To_Correct_Type()
    {
        // Arrange
        var sut = new Int32Aggregator();
        int value = 1;

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(value), new ConstantExpression(2L), (d1, d2) => d1 + d2);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(value + 2L);
    }

    [Fact]
    public void Aggregate_Returns_NonSuccess_When_SecondExpression_Evaluation_Fails()
    {
        // Arrange
        var sut = new Int32Aggregator();
        int value = 1;

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(value), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), (b1, b2) => b1 + b2);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }
}
