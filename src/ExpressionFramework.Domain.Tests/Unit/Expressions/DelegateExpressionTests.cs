namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class DelegateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new DelegateExpression(_ => "ok");

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("ok");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new DelegateExpressionBase(_ => null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new DelegateExpression(_ => null);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DelegateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(DelegateExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }
}
