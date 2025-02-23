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
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("Expression is not of type enumerable");
    }

    [Fact]
    public void Evaluate_Returns_Filtered_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new object?[] { "A", "B", 1, null, "C" }), new TypedConstantExpression<Type>(typeof(string)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        ((IEnumerable)result.Value!).OfType<object?>().ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void Evaluate_Returns_Error_When_TypeExpression_Returns_Error()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new object?[] { "A", "B", 1, null, "C" }), new TypedConstantResultExpression<Type>(Result.Error<Type>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new OfTypeExpression(new TypedConstantExpression<IEnumerable>(new object?[] { "A", "B", 1, null, "C" }), new TypedConstantExpression<Type>(typeof(string)));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<OfTypeExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OfTypeExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(OfTypeExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
