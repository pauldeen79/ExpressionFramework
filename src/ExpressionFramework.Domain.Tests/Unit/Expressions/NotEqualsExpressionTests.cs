namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class NotEqualsExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var firstExpression = new ErrorExpression(new TypedConstantExpression<string>("Kaboom"));
        var secondExpression = new ConstantExpression("2");
        var sut = new NotEqualsExpression(firstExpression, secondExpression);

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
        var secondExpression = new ErrorExpression(new TypedConstantExpression<string>("Kaboom"));
        var expression = new NotEqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = expression.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Correct_Value_On_Success()
    {
        // Arrange
        var expression = new NotEqualsExpressionBuilder()
            .WithFirstExpression(1)
            .WithSecondExpression(1)
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var firstExpression = new ErrorExpression(new TypedConstantExpression<string>("Kaboom"));
        var secondExpression = new ConstantExpression("2");
        var sut = new NotEqualsExpression(firstExpression, secondExpression);

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
        var secondExpression = new ErrorExpression(new TypedConstantExpression<string>("Kaboom"));
        var expression = new NotEqualsExpression(firstExpression, secondExpression);

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
        var expression = new NotEqualsExpressionBuilder()
            .WithFirstExpression(1)
            .WithSecondExpression(1)
            .BuildTyped();

        // Act
        var actual = expression.EvaluateTyped();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be(false);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new NotEqualsExpressionBuilder()
            .WithFirstExpression(true)
            .WithSecondExpression(false)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<NotEqualsExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new NotEqualsExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(NotEqualsExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(NotEqualsExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
