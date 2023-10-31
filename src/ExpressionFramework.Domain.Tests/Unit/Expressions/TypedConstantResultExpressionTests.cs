namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedConstantResultExpressionTests
{
    [Fact]
    public void Can_Evaluate_Value()
    {
        // Arrange
        var sut = new TypedConstantResultExpression<int>(Result.Success<int>(1));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(1);
    }

    [Fact]
    public void Can_Evaluate_Value_Typed()
    {
        // Arrange
        var sut = new TypedConstantResultExpression<int>(Result.Success<int>(1));

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
        var sut = new TypedConstantResultExpression<int>(Result.Success<int>(1));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<ConstantExpression>();
        ((ConstantExpression)actual).Value.Should().BeEquivalentTo(sut.Value.Value);
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new TypedConstantResultExpressionBase<bool>(Result.Success<bool>(default));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedConstantResultExpression<int>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TypedConstantResultExpression<int>));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
        result.ContextIsRequired.Should().BeNull();
    }

    [Fact]
    public void Can_Use_TypedConstantResultExpression_In_ExpressionBuilderFactory()
    {
        // Arrange
        var sut = new TypedConstantResultExpression<int>(Result.Success<int>(1));

        // Act
        var builder = ExpressionBuilderFactory.Create(sut);

        // Assert
        builder.Should().NotBeNull();
    }
}
