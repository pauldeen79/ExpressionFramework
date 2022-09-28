namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class EqualsExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_Evaluation_Of_FirstExpression_Fails()
    {
        // Arrange
        var firstExpression = new ErrorExpression("Kaboom");
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
        var secondExpression = new ErrorExpression("Kaboom");
        var expression = new EqualsExpression(firstExpression, secondExpression);

        // Act
        var actual = expression.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
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
