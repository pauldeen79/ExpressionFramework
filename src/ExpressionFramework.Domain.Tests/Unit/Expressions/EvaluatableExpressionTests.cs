namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EvaluatableExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Operator_Evaluation_Fails()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), new EqualsOperator(), new EmptyExpression()), new EmptyExpression());

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Result_When_Operator_Evaluation_Succeeds()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ConstantExpression("1"), new NotEqualsOperator(), new ConstantExpression("2")), new EmptyExpression());

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.GetValueOrThrow().ShouldBeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Operator_Evaluation_Fails()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), new EqualsOperator(), new EmptyExpression()), new EmptyExpression());

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Result_When_Operator_Evaluation_Succeeds()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ConstantExpression("1"), new NotEqualsOperator(), new ConstantExpression("1")), new EmptyExpression());

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.GetValueOrThrow().ShouldBe(false);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ConstantExpression("1"), new NotEqualsOperator(), new ConstantExpression("2")), new EmptyExpression());

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<EvaluatableExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(EvaluatableExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(EvaluatableExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBe(false);
    }
}
