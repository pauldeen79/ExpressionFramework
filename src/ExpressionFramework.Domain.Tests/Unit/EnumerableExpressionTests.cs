namespace ExpressionFramework.Domain.Tests.Unit;

public class EnumerableExpressionTests
{
    [Fact]
    public void GetDescriptor_Throws_On_Null_Type()
    {
        // Act & Assert
        this.Invoking(_ => EnumerableExpression.GetDescriptor(type: null!, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, typeof(object)))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void GetResultFromEnumerable_Returns_Invalid_On_Null_Expression()
    {
        // Act
        var result = EnumerableExpression.GetResultFromEnumerable(expression: null!, null, @delegate: x => x.Select(x => Result<object?>.Success(x)));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is required");
    }

    [Fact]
    public void GetResultFromEnumerable_Returns_Invalid_On_Null_Delegate()
    {
        // Act
        var result = EnumerableExpression.GetResultFromEnumerable(expression: new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object?>()), null, @delegate: null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Delegate is required");
    }
}
