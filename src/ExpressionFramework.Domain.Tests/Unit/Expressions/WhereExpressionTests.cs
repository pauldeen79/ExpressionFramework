namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class WhereExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Predicate_Does_Not_Return_A_Boolean_Value()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(_ => "not a boolean value"));

        // Act
        var result = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Predicate()
    {
        // Arrange
        var sut = new WhereExpression(new ErrorExpression("Kaboom"));

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
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var result = sut.Evaluate(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] {"A", "B", "C" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var resul = sut.ValidateContext(44);

        // Assert
        resul.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Predicate_Does_Not_Return_A_Boolean_Value()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(_ => "not a boolean value"));

        // Act
        var result = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[]
        {
            "Predicate dit not return a boolean value on item 0",
            "Predicate dit not return a boolean value on item 1",
            "Predicate dit not return a boolean value on item 2",
            "Predicate dit not return a boolean value on item 3"
        });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Predicate_Returns_Status_Invalid()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateResultExpression(x => x is string s && s == "C" ? Result<object?>.Invalid("Value C is not supported") : Result<object?>.Success(true)));

        // Act
        var result = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Predicate returned an invalid result on item 3. Error message: Value C is not supported" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new WhereExpression(new DelegateExpression(x => x is string));

        // Act
        var result = sut.ValidateContext(new object[] { "A", "B", 1, "C" });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(WhereExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(WhereExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(3);
        result.ContextIsRequired.Should().BeTrue();
    }
}
