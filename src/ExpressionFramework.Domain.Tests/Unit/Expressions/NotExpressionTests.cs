namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NotExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpressionBuilder()
            .WithExpression(false)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(true);
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var expression = new NotExpression(new TypedConstantResultExpression<bool>(Result.Error<bool>("Kaboom")));

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_With_Negated_BooleanValue()
    {
        // Arrange
        var sut = new NotExpression(new TypedConstantExpression<bool>(true));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(false);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new NotExpression(new TrueExpression());

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<NotExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(NotExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(NotExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
