namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AggregateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Contains_No_Items()
    {
        // Arrange
        var sut = new AggregateExpressionBuilder().WithAggregator(new AddAggregatorBuilder()).Build();

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
        var sut = new AggregateExpressionBuilder().AddExpressions(1, 2, 3).WithAggregator(new AddAggregatorBuilder()).Build();

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
        var sut = new AggregateExpressionBuilder()
            .AddExpressions(new TypedConstantResultExpressionBuilder<int>().WithValue(Result<int>.Error("Kaboom")), new TypedConstantExpressionBuilder<int>().WithValue(1))
            .WithAggregator(new AddAggregatorBuilder())
            .Build();

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
        var sut = new AggregateExpressionBuilder()
            .AddExpressions(new ConstantExpressionBuilder().WithValue(1), new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom")))
            .WithAggregator(new AddAggregatorBuilder())
            .Build();

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
        var expression = new AggregateExpressionBase(Enumerable.Empty<Expression>(), new AddAggregator(), default);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
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
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(3);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.UsesContext.Should().BeTrue();
        result.ContextIsRequired.Should().BeFalse();
    }
}
