namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class AggregatorBaseTests
{
    [Fact]
    public void Aggregate_Returns_Invalid_Result_On_Null_FirstExpression()
    {
        // Act
        var result = AggregatorBase.Aggregate(null, firstExpression: null!, secondExpression: new EmptyExpression(), new TypedConstantExpression<IFormatProvider>(CultureInfo.InvariantCulture), (_, _, _) => Result<object?>.Success(default));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("First expression is required");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_Result_On_Null_SecondExpression()
    {
        // Act
        var result = AggregatorBase.Aggregate(null, firstExpression: new EmptyExpression(), secondExpression: null!, new TypedConstantExpression<IFormatProvider>(CultureInfo.InvariantCulture), (_, _, _) => Result<object?>.Success(default));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Second expression is required");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_On_Null_AggregateExpression()
    {
        // Act
        var result = AggregatorBase.Aggregate(null, firstExpression: new EmptyExpression(), secondExpression: new EmptyExpression()!, new TypedConstantExpression<IFormatProvider>(CultureInfo.InvariantCulture), aggregateDelegate: null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Aggregate expression is required");
    }
}
