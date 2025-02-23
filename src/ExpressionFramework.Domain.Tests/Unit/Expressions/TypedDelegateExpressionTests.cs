namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedDelegateExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new TypedDelegateExpression<int>(_ => 34);

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(34);
    }

    [Fact]
    public void EvaluateTyped_Returns_Value_From_Delegate()
    {
        // Arrange
        var sut = new TypedDelegateExpression<int>(_ => 34);

        // Act
        var result = sut.EvaluateTyped("not used");

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(34);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedDelegateExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TypedDelegateExpression<int>));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
