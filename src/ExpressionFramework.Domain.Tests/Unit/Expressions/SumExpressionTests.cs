namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SumExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SelectorExpression_Returns_Error()
    {
        // Arrange
        var sut = new SumExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Enumerable_Values_Are_Not_All_Numeric()
    {
        // Arrange
        var sut = new SumExpressionBuilder()
            .WithExpression(new[] { "A", "B", "C" })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could only compute sum of numeric values");
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Byte_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder()
            .WithExpression(new byte[] { 1, 2 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Short_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder()
            .WithExpression(new[] { (short)1, (short)2 })
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Int_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder().WithExpression(new[] { 1, 2 }).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Long_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder().WithExpression(new long[] { 1, 2 }).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Float_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder().WithExpression(new[] { (float)1.5, (float)2.25 }).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Double_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder().WithExpression(new[] { 1.5, 2.25 }).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Decimal_Values_When_SelectorExpression_Is_Null()
    {
        // Arrange
        var sut = new SumExpressionBuilder().WithExpression(new[] { (decimal)1.5, (decimal)2.25 }).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3.75);
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_SelectorExpression_Result_When_SelectorExpression_Is_Not_Null()
    {
        // Arrange
        var sut = new SumExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2 }), new DelegateExpression(x => Convert.ToInt32(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(3);
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
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextIsRequired.Should().BeNull();
    }
}
