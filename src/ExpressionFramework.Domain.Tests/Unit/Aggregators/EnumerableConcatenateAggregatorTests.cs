namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class EnumerableConcatenateAggregatorTests
{
    [Fact]
    public void Aggregate_Returns_Invalid_When_FirstExpression_Is_Not_Enumerable()
    {
        // Arrange
        var sut = new EnumerableConcatenateAggregator();

        // Act
        var result = sut.Aggregate(null, new EmptyExpression(), new ConstantExpression(new[] { "b" }));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("First expression is not of type enumerable");
    }

    [Fact]
    public void Aggregate_Returns_Error_When_SecondExpression_Returns_Error()
    {
        // Arrange
        var sut = new EnumerableConcatenateAggregator();

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(new[] { "a" }), new ErrorExpression(new ConstantExpression("Kaboom")));

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Aggregate_Returns_Invalid_When_SecondExpression_Result_Value_Is_Not_Enumerable()
    {
        // Arrange
        var sut = new EnumerableConcatenateAggregator();

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(new[] { "a" }), new ConstantExpression(1));

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Second expression is not of type enumerable");
    }

    [Fact]
    public void Aggregate_Returns_Concatenated_Value_When_Context_And_SecondExpression_Result_Value_Are_String()
    {
        // Arrange
        var sut = new EnumerableConcatenateAggregator();

        // Act
        var result = sut.Aggregate(null, new ConstantExpression(new[] { "a" }), new ConstantExpression(new[] { "b" }));

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "a", "b" });
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(EnumerableConcatenateAggregator));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EnumerableConcatenateAggregator));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
        result.ContextDescription.Should().BeEmpty();
    }
}
