namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class SubtractAggregatorTests
{
    [Fact]
    public void Aggregate_Return_Invalid_When_Using_Unsupported_Context_Type()
    {
        // Arrange
        var sut = new SubtractAggregator();

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
        var sut = new SubtractAggregator();
        byte input = 1;

        // Act
        var result = sut.Aggregate(input, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int16()
    {
        // Arrange
        var sut = new SubtractAggregator();
        short input = 1;

        // Act
        var result = sut.Aggregate(input, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int32()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(1, new ConstantExpression(2));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 - 2);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int64()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(1L, new ConstantExpression(2L));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1L - 2L);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Float()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(1f, new ConstantExpression(2f));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1f - 2f);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Double()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(1d, new ConstantExpression(2d));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1d - 2d);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Decimal()
    {
        // Arrange
        var sut = new SubtractAggregator();

        // Act
        var result = sut.Aggregate(1M, new ConstantExpression(2M));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1M - 2M);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(SubtractAggregator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SubtractAggregator));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
    }
}
