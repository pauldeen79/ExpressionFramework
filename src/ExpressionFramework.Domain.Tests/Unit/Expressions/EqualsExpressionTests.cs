namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EqualsExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var firstExpression = new ErrorExpression(new ConstantExpression("Kaboom"));
        var secondExpression = new ConstantExpression("2");
        var sut = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Evaluation_Of_SecondExpression_Fails()
    {
        // Arrange
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ErrorExpression(new ConstantExpression("Kaboom"));
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = expression.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var firstExpression = new ErrorExpression(new ConstantExpression("Kaboom"));
        var secondExpression = new ConstantExpression("2");
        var sut = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Evaluation_Of_SecondExpression_Fails()
    {
        // Arrange
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ErrorExpression(new ConstantExpression("Kaboom"));
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = expression.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Correct_Value_On_Success()
    {
        // Arrange
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ConstantExpression("1");
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = expression.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be(true);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new EqualsExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var firstExpression = new ConstantExpression("1");
        var secondExpression = new ConstantExpression("1");
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(EqualsExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(EqualsExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
