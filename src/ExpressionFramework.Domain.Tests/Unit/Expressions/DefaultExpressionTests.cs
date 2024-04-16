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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(default(int));
    }

    [Fact]
    public void Evaluate_Returns_Default_Value_Of_Nullable_Int()
    {
        // Arrange
        var sut = new DefaultExpression<int?>();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(default(int?));
    }

    [Fact]
    public void Evaluate_Returns_Default_Value_Of_String()
    {
        // Arrange
        var sut = new DefaultExpression<string>();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(default(string));
    }

    [Fact]
    public void ToUntyped_Returns_ConstantExpression()
    {
        // Arrange
        var expression = new DefaultExpressionBuilder<IEnumerable>().BuildTyped();

        // Act
        var untypedExpression = expression.ToUntyped();

        // Assert
        untypedExpression.Should().BeOfType<ConstantExpression>().Which.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(DefaultExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(DefaultExpression<int>));
        result.Parameters.Should().BeEmpty();
        result.ReturnValues.Should().ContainSingle();
    }
}
