namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedConstantExpressionTests
{
    [Fact]
    public void Can_Evaluate_Value()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1);
    }

    [Fact]
    public void Can_Evaluate_Value_Typed()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(1);
    }

    [Fact]
    public void Can_Convert_To_Untyped_ConstantExpression()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<ConstantExpression>();
        ((ConstantExpression)actual).Value.ShouldBeEquivalentTo(sut.Value);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedConstantExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TypedConstantExpression<int>));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
        result.ContextIsRequired.ShouldBeNull();
    }
}
