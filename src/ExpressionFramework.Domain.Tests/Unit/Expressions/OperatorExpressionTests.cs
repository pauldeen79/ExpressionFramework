namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OperatorExpressionTests
{
    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression()
    {
        // Arrange
        var @operator = new EqualsOperatorBuilder();
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression("A")
            .WithRightExpression("B")
            .WithOperator(@operator)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(false);
    }

    [Fact]
    public void Can_Evaluate_Operator_Through_OperatorExpression_Typed()
    {
        // Arrange
        var @operator = new EqualsOperatorBuilder();
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression("A")
            .WithRightExpression("B")
            .WithOperator(@operator)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeFalse();
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new OperatorExpressionBuilder()
            .WithLeftExpression(false)
            .WithRightExpression(true)
            .WithOperator(new EqualsOperatorBuilder())
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<OperatorExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OperatorExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(OperatorExpression));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.ContextIsRequired.ShouldBe(false);
    }
}
