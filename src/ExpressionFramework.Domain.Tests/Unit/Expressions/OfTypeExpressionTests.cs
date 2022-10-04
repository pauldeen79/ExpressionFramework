namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OfTypeExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.Evaluate(1);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeEquivalentTo(new[] {"A", "B", "C" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.ValidateContext(44);

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new OfTypeExpression(typeof(string));

        // Act
        var actual = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OfTypeExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OfTypeExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
