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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("Hello world");
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
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("SourceExpression is not of type System.Collections.IEnumerable");
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
        untypedExpression.ShouldBeOfType<ConstantExpression>().Value.ShouldBe(1);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(CastExpression<IEnumerable>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(CastExpression<IEnumerable>));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
