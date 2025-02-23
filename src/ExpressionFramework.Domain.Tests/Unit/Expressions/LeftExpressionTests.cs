namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class LeftExpressionTests
{
    [Fact]
    public void Evaluate_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(2)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("te");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression(string.Empty)
            .WithLengthExpression(2)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedDelegateResultExpression<string>(_ => Result.Error<string>("Kaboom")), new TypedConstantExpression<int>(1));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedDelegateResultExpression<int>(_ => Result.Error<int>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_LeftValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("te");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>(string.Empty), new TypedConstantExpression<int>(2));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new LeftExpression(new TypedConstantExpression<string>("test"), new TypedDelegateResultExpression<int>(_ => Result.Error<int>("Kaboom")));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new LeftExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<LeftExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(LeftExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(LeftExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
