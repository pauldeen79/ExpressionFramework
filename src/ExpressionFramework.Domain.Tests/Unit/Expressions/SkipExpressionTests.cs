namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SkipExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_CountExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression("non integer value"));

        // Act
        var result = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("CountExpression did not return an integer");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_CountExpression_Returns_Error()
    {
        // Arrange
        var sut = new SkipExpression(new ErrorExpression("Kaboom"));

        // Act
        var result = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new object[] { "B", 1, "C" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_CountExpression_Returns_Invalid_Result()
    {
        // Arrange
        var sut = new SkipExpression(new InvalidExpression("Some error message"));

        // Act
        var result = sut.ValidateContext(new[] { "A", "B", "C" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "CountExpression returned an invalid result. Error message: Some error message" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_CountExpression_Returns_Non_Integer_Value()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression("non integer value"));

        // Act
        var result = sut.ValidateContext(new[] { "A", "B", "C" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "CountExpression did not return an integer" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SkipExpression(1);

        // Act
        var result = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_CountExpression_Returns_Error_Result()
    {
        // Arrange
        var sut = new SkipExpression(new ErrorExpression("Kaboom"));

        // Act
        var result = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SkipExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SkipExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
