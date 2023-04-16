namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AggregateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Contains_No_Items()
    {
        // Arrange
        var sut = new AggregateExpression(Enumerable.Empty<object?>(), new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Contains_No_Items_Using_Delegates()
    {
        // Arrange
        var sut = new AggregateExpression(Enumerable.Empty<Func<object?, object?>>(), new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Sequence contains no elements");
    }

    [Fact]
    public void Evaluate_Returns_Aggregation_Of_FirstExpression_And_SecondExpression()
    {
        // Arrange
        var sut = new AggregateExpression(new object?[] { 1, 2, 3 }, new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2 + 3);
    }

    [Fact]
    public void Evaluate_Returns_Aggregation_Of_FirstExpression_And_SecondExpression_Using_Delegates()
    {
        // Arrange
        var sut = new AggregateExpression(new Func<object?, object?>[] { _ => 1, _ => 2, _ => 3 }, new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1 + 2 + 3);
    }

    [Fact]
    public void Evaluate_Returns_Error_When_FirstExpression_Returns_Error()
    {
        // Arrange
        var sut = new AggregateExpression(new Expression[] { new ErrorExpression(new ConstantExpression("Kaboom")), new ConstantExpression(1) }, new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SecondExpression_Returns_Error()
    {
        // Arrange
        var sut = new AggregateExpression(new Expression[] { new ConstantExpression(1), new ErrorExpression(new ConstantExpression("Kaboom")) }, new AddAggregator());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new AggregateExpressionBase(Enumerable.Empty<Expression>(), new AddAggregator());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new AggregateExpression(new object[] { 1, 2, 3 }.Select(x => new ConstantExpression(x)), new AddAggregator());

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AggregateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(AggregateExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
