namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SkipExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(1));

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(1));

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
        var sut = new SkipExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(1));

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SkipExpression(new ConstantExpression(1));

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
