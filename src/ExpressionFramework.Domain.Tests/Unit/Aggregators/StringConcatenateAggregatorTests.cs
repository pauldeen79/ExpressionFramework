﻿namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class StringConcatenateAggregatorTests
{
    [Fact]
    public void Aggregate_Returns_Invalid_When_FirstExpression_Is_Not_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate(new EmptyExpression(), new ConstantExpression("b"));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("First expression is not of type string");
    }

    [Fact]
    public void Aggregate_Returns_Error_When_SecondExpression_Returns_Error()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression("a"), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_When_SecondExpression_Result_Value_Is_Not_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression("a"), new ConstantExpression(1));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Second expression is not of type string");
    }

    [Fact]
    public void Aggregate_Returns_Concatenated_Value_When_Context_And_SecondExpression_Result_Value_Are_String()
    {
        // Arrange
        var sut = new StringConcatenateAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression("a"), new ConstantExpression("b"));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("ab");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(StringConcatenateAggregator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(StringConcatenateAggregator));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeEmpty();
    }
}
