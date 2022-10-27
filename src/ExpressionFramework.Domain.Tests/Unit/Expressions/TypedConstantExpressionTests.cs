namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedConstantExpressionTests
{
    [Fact]
    public void Can_Evaluate_Value()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Can_Evaluate_Value_Typed()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var result = sut.EvaluateTyped(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1);
    }

    [Fact]
    public void Can_Convert_To_Untyped_ConstantExpression()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<ConstantExpression>();
        ((ConstantExpression)actual).Value.Should().BeEquivalentTo(sut.Value);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedConstantExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TypedConstantExpression<int>));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }

    [Fact]
    public void Can_Use_TypedConstantExpression_In_ExpressionBuilderFactory()
    {
        // Arrange
        var sut = new TypedConstantExpression<int>(1);

        // Act
        var builder = ExpressionBuilderFactory.Create(sut);

        // Assert
        builder.Should().NotBeNull();
    }
}
