namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SelectExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Predicate()
    {
        // Arrange
        var sut = new SelectExpression(new ErrorExpression("Kaboom"));

        // Act
        var result = sut.Evaluate(new[] { "a", "b", "c" });

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.Evaluate(new[] { "a", "b", "c" });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Predicate_Returns_Status_Invalid()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.ValidateContext(new object[] { "a", "b", 1, "c" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "SelectExpression returned an invalid result on item 2. Error message: Context must be of type string" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new ToUpperCaseExpression());

        // Act
        var result = sut.ValidateContext(new[] { "a", "b", "c" });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SelectExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SelectExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
