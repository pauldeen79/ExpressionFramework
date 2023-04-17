namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ErrorExpressionTests
{
    [Fact]
    public void Evaluate_Returns_ErrorResult()
    {
        // Assert
        var sut = new ErrorExpression("Error message");

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Error message");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new ErrorExpression(new TypedErrorConstantExpression<string>("Kaboom"));
        //var sut = new ErrorExpression(new TypedDelegateResultExpression<string>(_ => new ErrorExpression("Kaboom")));

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
        var expression = new ErrorExpressionBase(new TypedConstantExpression<string>(string.Empty));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported_ConstantExpression()
    {
        // Arrange
        var expression = new ErrorExpression("Kaboom");

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported_DelegateExpression()
    {
        // Arrange
        var expression = new ErrorExpression(_ => "Kaboom");

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ErrorExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ErrorExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
