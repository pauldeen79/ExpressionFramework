namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TrueExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_With_Value_True()
    {
        // Arrange
        var sut = new TrueExpression();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluatTyped_Returns_Success_With_Value_True()
    {
        // Arrange
        var sut = new TrueExpression();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new TrueExpressionBase();

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new TrueExpression();

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrueExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TrueExpression));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
