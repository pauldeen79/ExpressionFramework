namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SumExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context cannot be empty");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(12345);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context must be of type IEnumerable");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SelectorExpression_Returns_Error()
    {
        // Arrange
        var sut = new SumExpression(new ErrorExpression("Kaboom"));

        // Act
        var result = sut.Evaluate(new[] { 1, 2, 3 });

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Enumerable_Values_Are_Not_All_Numeric()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { "A", "B", "C" });

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could only compute sum of numeric values");
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Byte_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { (byte)1, (byte)2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Short_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { (short)1, (short)2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Int_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Long_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { (long)1, (long)2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Float_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { (float)1.5, (float)2.25 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Double_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { 1.5, 2.25 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Decimal_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.Evaluate(new[] { (decimal)1.5, (decimal)2.25 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_SelectorExpression_Result_When_SelectorExpression_Is_Not_Null()
    {
        // Arrange
        var sut = new SumExpression(new DelegateExpression(x => Convert.ToInt32(x)));

        // Act
        var result = sut.Evaluate(new[] { 1, 2 });

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.ValidateContext(12345);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Enumerable_Values_Are_Not_All_Numeric()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.ValidateContext(new object[] { 1, 2, "C" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Could only compute sum of numeric values" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SumExpression(null);

        // Act
        var result = sut.ValidateContext(new object[] { 1, 2, 3 });

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SumExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SumExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(3);
        result.ContextIsRequired.Should().BeTrue();
    }
}
