namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EvaluatableExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Operator_Evaluation_Fails()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ErrorExpression(new ConstantExpression("Kaboom")), new EqualsOperator(), new EmptyExpression()));

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Result_When_Operator_Evaluation_Succeeds()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ConstantExpression("1"), new NotEqualsOperator(), new ConstantExpression("2")));

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.GetValueOrThrow().Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Operator_Evaluation_Fails()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ErrorExpression(new ConstantExpression("Kaboom")), new EqualsOperator(), new EmptyExpression()));

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Result_When_Operator_Evaluation_Succeeds()
    {
        // Arrange
        var sut = new EvaluatableExpression(new SingleEvaluatable(new ConstantExpression("1"), new NotEqualsOperator(), new ConstantExpression("1")));

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.GetValueOrThrow().Should().Be(false);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(EvaluatableExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EvaluatableExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeFalse();
    }
}
