namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CastExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_When_SourceExpression_Can_Be_Cast()
    {
        // Arrange
        var expression = new CastExpressionBuilder<IEnumerable>()
            .WithSourceExpression("Hello world")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_SourceExpression_Cannot_Be_Cast()
    {
        // Arrange
        var expression = new CastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SourceExpression is not of type System.Collections.IEnumerable");
    }

    [Fact]
    public void ToUntyped_Returns_SourceExpression()
    {
        // Arrange
        var expression = new CastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .BuildTyped();

        // Act
        var untypedExpression = expression.ToUntyped();

        // Assert
        untypedExpression.Should().BeOfType<ConstantExpression>().Which.Value.Should().Be(1);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(CastExpression<IEnumerable>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(CastExpression<IEnumerable>));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new CastExpressionBase<IEnumerable>(new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }
}
