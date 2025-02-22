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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("ok");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DelegateExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(DelegateExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
