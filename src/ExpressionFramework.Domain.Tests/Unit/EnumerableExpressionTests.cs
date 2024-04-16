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
        var result = EnumerableExpression.GetResultFromEnumerable(expression: null!, null, @delegate: x => x.Select(x => Result.Success(x)));

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

    [Fact]
    public void GetTypedResultFromEnumerable_Ruturns_Invalid_On_Null_Expression()
    {
        // Act
        var result = EnumerableExpression.GetTypedResultFromEnumerable(expression: null!, null, @delegate: x => x.Select(x => Result.Success(x)));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is required");
    }

    [Fact]
    public void GetTypedResultFromEnumerable_Ruturns_Invalid_On_Null_Delegate()
    {
        // Act
        var result = EnumerableExpression.GetTypedResultFromEnumerable(expression: new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object?>()), null, @delegate: null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Delegate is required");
    }

    [Fact]
    public void GetTypedResultFromEnumerableWithCount_Ruturns_Invalid_On_Null_CountExpression()
    {
        // Act
        var result = EnumerableExpression.GetTypedResultFromEnumerableWithCount(new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object?>()), countExpression: null!, null, (_, _) => Enumerable.Empty<Result<object?>>());

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Count expression is required");
    }

    [Fact]
    public void GetTypedResultFromEnumerableWithCount_Ruturns_Invalid_On_Null_Delegate()
    {
        // Act
        var result = EnumerableExpression.GetTypedResultFromEnumerableWithCount(new TypedConstantExpression<IEnumerable>(Enumerable.Empty<object?>()), new TypedConstantExpression<int>(23), null, @delegate: null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Delegate is required");
    }

    [Fact]
    public void GetRequiredScalarValue_Returns_Invalid_On_Null_DelegateWithoutPredicate()
    {
        // Act
        var result = EnumerableExpression.GetRequiredScalarValue<int>(null, new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), null, delegateWithoutPredicate: null!);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Delegate without predicate is required");
    }
}
