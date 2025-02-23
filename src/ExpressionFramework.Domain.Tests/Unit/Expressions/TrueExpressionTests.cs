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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(true);
    }

    [Fact]
    public void EvaluatTyped_Returns_Success_With_Value_True()
    {
        // Arrange
        var sut = new TrueExpression();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(true);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new TrueExpression();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<TrueExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TrueExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TrueExpression));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
