namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class SubtractAggregatorTests
{
    [Fact]
    public void Aggregate_Return_Invalid_When_Using_Unsupported_FirstExpression()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(true), new ConstantExpression(1));

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("First expression is not of a supported type");
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Byte()
    {
        // Arrange
        var sut = new SubtractAggregator();
        byte input = 1;

        // Act
        var result = sut.Aggregate(new ConstantExpression(input), new ConstantExpression(2));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int16()
    {
        // Arrange
        var sut = new SubtractAggregator();
        short input = 1;

        // Act
        var result = sut.Aggregate(new ConstantExpression(input), new ConstantExpression(2));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int32()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1), new ConstantExpression(2));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int64()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1L), new ConstantExpression(2L));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1L - 2L);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Float()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1f), new ConstantExpression(2f));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1f - 2f);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Double()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1d), new ConstantExpression(2d));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1d - 2d);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Decimal()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1M), new ConstantExpression(2M));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1M - 2M);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(SubtractAggregator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(SubtractAggregator));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeEmpty();
    }
}
