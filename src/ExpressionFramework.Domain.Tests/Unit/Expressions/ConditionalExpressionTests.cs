namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ConditionalExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_From_Expression_Evaluation()
    {
        // Arrange
        var sut = new ConditionalExpression(new SingleEvaluatable(new ErrorExpression("Kaboom"), new EqualsOperator(), new EmptyExpression()), new EmptyExpression(), null);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateWithConditionResult_Returns_Error_From_Expression_Evaluation()
    {
        // Arrange
        var sut = new ConditionalExpression(new SingleEvaluatable(new ErrorExpression("Kaboom"), new EqualsOperator(), new EmptyExpression()), new EmptyExpression(), null);

        // Act
        var result = sut.EvaluateWithConditionResult(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateWithConditionResult_Returns_DefaultExpression_Result_When_Available_And_ConditionEvalution_Returns_False()
    {
        // Arrange
        var sut = new ConditionalExpression(new SingleEvaluatable(new ConstantExpression("Something"), new EqualsOperator(), new ConstantExpression("Something else")), new EmptyExpression(), new ConstantExpression("Default value"));

        // Act
        var result = sut.EvaluateWithConditionResult(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.ConditionResult.Should().BeFalse();
        result.Value.ExpressionResult.Status.Should().Be(ResultStatus.Ok);
        result.Value.ExpressionResult.Value.Should().Be("Default value");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ConditionalExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ConditionalExpression));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }
}
