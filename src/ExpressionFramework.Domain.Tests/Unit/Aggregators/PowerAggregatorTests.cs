namespace ExpressionFramework.Domain.Tests.Unit.Aggregators;

public class PowerAggregatorTests
{
    [Fact]
    public void Aggregate_Return_Invalid_When_Using_Unsupported_FirstExpression()
    {
        // Arrange
        var sut = new PowerAggregator();

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
        var sut = new PowerAggregator();
        byte input = 2;

        // Act
        var result = sut.Aggregate(new ConstantExpression(input), new ConstantExpression(4));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(16);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int16()
    {
        // Arrange
        var sut = new PowerAggregator();
        short input = 2;

        // Act
        var result = sut.Aggregate(new ConstantExpression(input), new ConstantExpression(8));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(256);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int32()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(2), new ConstantExpression(8));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(256);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Int64()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(2L), new ConstantExpression(8L));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(256L);
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Float()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1f), new ConstantExpression(2f));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(Math.Pow(1f, 2f));
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Double()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1d), new ConstantExpression(2d));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(Math.Pow(1d, 2d));
    }

    [Fact]
    public void Aggregate_Returns_Correct_Result_On_Decimal()
    {
        // Arrange
        var sut = new PowerAggregator();

        // Act
        var result = sut.Aggregate(new ConstantExpression(1M), new ConstantExpression(2M));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo((decimal)Math.Pow(Convert.ToDouble(1M), Convert.ToDouble(2M)));
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(PowerAggregator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(PowerAggregator));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeEmpty();
    }
}
