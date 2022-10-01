﻿namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class PowerAggregatorTests
{
    [Fact]
    public void Aggregate_Return_Invalid_When_Using_Unsupported_Context_Type()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(true, new ConstantExpression(1));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context is not of a supported type");
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Byte()
    {
        // Arrange
        var sut = new PowerAggregator();
        byte input = 1;

        // Act
        var result = sut.Aggregate(input, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 ^ 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int16()
    {
        // Arrange
        var sut = new PowerAggregator();
        short input = 1;

        // Act
        var result = sut.Aggregate(input, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 ^ 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int32()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(1, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 ^ 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int64()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(1L, new ConstantExpression(2L));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1L ^ 2L);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Float()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(1f, new ConstantExpression(2f));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(Math.Pow(1f, 2f));
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Double()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(1d, new ConstantExpression(2d));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(Math.Pow(1d, 2d));
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Decimal()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(1M, new ConstantExpression(2M));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(Math.Pow(Convert.ToDouble(1M), Convert.ToDouble(2M)));
    }
}
