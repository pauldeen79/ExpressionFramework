namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class DelegateResultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Value_From_Delegate_Status_Ok()
    {
        // Arrange
        var sut = new DelegateResultExpression(_ => Result.Success<object?>("ok"));

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("ok");
    }

    [Fact]
    public void Evaluate_Returns_Value_From_Delegate_Status_Error()
    {
        // Arrange
        var sut = new DelegateResultExpression(_ => Result.Error<object?>("Kaboom"));

        // Act
        var result = sut.Evaluate("not used");

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DelegateResultExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(DelegateResultExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
