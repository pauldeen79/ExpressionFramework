namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OfTypeExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(default(IEnumerable)!), new TypedConstantExpression<Type>(typeof(string)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new object?[] { "A", "B", 1, null, "C" }), new TypedConstantExpression<Type>(typeof(string)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(new[] { "A", "B", "C" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new object?[] { "A", "B", 1, null, "C" }), new TypedConstantExpression<Type>(typeof(string)));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<OfTypeExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new OfTypeExpressionBase(new TypedConstantExpression<IEnumerable>(default(IEnumerable)!), new TypedConstantExpression<Type>(typeof(string)));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new[] { 1, 2, 3 }), new TypedConstantExpression<Type>(typeof(int)));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OfTypeExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OfTypeExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
