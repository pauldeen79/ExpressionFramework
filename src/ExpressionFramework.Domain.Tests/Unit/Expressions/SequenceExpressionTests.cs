namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SequenceExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_One_Expression_Returns_Invalid()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2)
            .AddExpressions(new InvalidExpressionBuilder().WithErrorMessageExpression("Message"))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Message");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_One_Expression_Returns_Error()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2)
            .AddExpressions(new ErrorExpressionBuilder().WithErrorMessageExpression("Kaboom"))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Empty_Sequence_When_Expressions_Are_Empty()
    {
        // Arrange
        var sut = new SequenceExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.Evaluate(null).TryCast<IEnumerable<object?>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Filled_Sequence_When_Expressions_Are_Not_Empty()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2, 3)
            .Build();

        // Act
        var result = sut.Evaluate(null).TryCast<IEnumerable<object?>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value!.ToArray().ShouldBeEquivalentTo(new object[] { 1, 2, 3 });
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_One_Expression_Returns_Invalid()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2)
            .AddExpressions(new InvalidExpressionBuilder().WithErrorMessageExpression("Message"))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Message");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_One_Expression_Returns_Error()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2)
            .AddExpressions(new ErrorExpressionBuilder().WithErrorMessageExpression("Kaboom"))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Empty_Sequence_When_Expressions_Are_Empty()
    {
        // Arrange
        var sut = new SequenceExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEmpty();
    }

    [Fact]
    public void EvaluateTyped_Returns_Filled_Sequence_When_Expressions_Are_Not_Empty()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2, 3)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value!.ToArray().ShouldBeEquivalentTo(new object[] { 1, 2, 3 });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SequenceExpressionBuilder()
            .AddExpressions(1, 2, 3)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<SequenceExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SequenceExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(SequenceExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
