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
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new DefaultExpressionBase<int>();

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
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
