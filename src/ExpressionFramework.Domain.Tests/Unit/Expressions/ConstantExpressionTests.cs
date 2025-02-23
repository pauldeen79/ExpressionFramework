namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ConstantExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value()
    {
        // Arrange
        var expression = new ConstantExpressionBuilder().WithValue("The value").Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("The value");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ConstantExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ConstantExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
