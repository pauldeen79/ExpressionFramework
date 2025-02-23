namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class RightExpressionTests
{
    [Fact]
    public void Evaluate_Returns_RightValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new RightExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(2)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("st");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new RightExpressionBuilder()
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
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new RightExpressionBuilder()
            .WithExpression(default(string)!)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new RightExpression(new TypedConstantExpression<string>("test"), new TypedConstantResultExpression<int>(Result.Error<int>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_RightValue_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new RightExpressionBuilder()
            .WithExpression("test")
            .WithLengthExpression(2)
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("st");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new RightExpressionBuilder()
            .WithExpression(string.Empty)
            .WithLengthExpression(2)
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new RightExpressionBuilder().WithExpression(default(string)!).BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Error_When_LengthExpression_Evaluation_Returns_Error()
    {
        // Arrange
        var sut = new RightExpression(new TypedConstantExpression<string>("test"), new TypedConstantResultExpression<int>(Result.Error<int>("Kaboom")));

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
        var sut = new RightExpressionBuilder()
            .WithExpression("some test")
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<RightExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(RightExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(RightExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
