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
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Sequence contains no elements");
    }

    [Fact]
    public void Evaluate_Returns_Aggregation_Of_FirstExpression_And_SecondExpression()
    {
        // Arrange
        var sut = new AggregateExpressionBuilder().AddExpressions(1, 2, 3).WithAggregator(new AddAggregatorBuilder()).Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 + 2 + 3);
    }


    [Fact]
    public void Evaluate_Returns_Error_When_FirstExpression_Returns_Error()
    {
        // Arrange
        var sut = new AggregateExpressionBuilder()
            .AddExpressions(new TypedConstantResultExpressionBuilder<int>().WithValue(Result.Error<int>("Kaboom")), new TypedConstantExpressionBuilder<int>().WithValue(1))
            .WithAggregator(new AddAggregatorBuilder())
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
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
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AggregateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(AggregateExpression));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.Count.ShouldBe(3);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.UsesContext.ShouldBeTrue();
        result.ContextIsRequired.ShouldBe(false);
    }
}
