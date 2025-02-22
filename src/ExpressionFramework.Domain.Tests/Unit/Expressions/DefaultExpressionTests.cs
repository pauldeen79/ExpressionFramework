namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class DefaultExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Default_Value_Of_Int()
    {
        // Arrange
        var sut = new DefaultExpression<int>();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(default(int));
    }

    [Fact]
    public void Evaluate_Returns_Default_Value_Of_Nullable_Int()
    {
        // Arrange
        var sut = new DefaultExpression<int?>();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(default(int?));
    }

    [Fact]
    public void Evaluate_Returns_Default_Value_Of_String()
    {
        // Arrange
        var sut = new DefaultExpression<string>();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe(default(string));
    }

    [Fact]
    public void ToUntyped_Returns_ConstantExpression()
    {
        // Arrange
        var expression = new DefaultExpressionBuilder<IEnumerable>().BuildTyped();

        // Act
        var untypedExpression = expression.ToUntyped();

        // Assert
        untypedExpression.ShouldBeOfType<ConstantExpression>().Value.ShouldBeNull();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DefaultExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(DefaultExpression<int>));
        result.Parameters.ShouldBeEmpty();
        result.ReturnValues.ShouldHaveSingleItem();
    }
}
