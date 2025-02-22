namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SubstringExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Substring_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression("test")
            .WithIndexExpression(1)
            .WithLengthExpression(1)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.GetValueOrThrow().ShouldBeEquivalentTo("e");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression(string.Empty)
            .WithIndexExpression(1)
            .WithLengthExpression(1)
            .Build();

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Index and length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(new DefaultExpression<string>(), new DefaultExpression<int>(), new DefaultExpression<int>());

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new TypedConstantExpression<string>("test"), new TypedConstantResultExpression<int>(Result.Invalid<int>("Kaboom")), default);

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(1), new TypedConstantResultExpression<int>(Result.Invalid<int>("Kaboom")));

        // Act
        var actual = sut.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Substring_From_Expression_When_Expression_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression("test")
            .WithIndexExpression(1)
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("e");
    }

    [Fact]
    public void EvaluateTyped_Returns_Substring_From_Expression_When_Expression_Is_NonEmptyString_No_Length()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression("test")
            .WithIndexExpression(1)
            .WithLengthExpression((int?)null)
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("est");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression(string.Empty)
            .WithIndexExpression(1)
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Index and length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(new DefaultExpression<string>(), new DefaultExpression<int>(), new DefaultExpression<int>());

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression is not of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new TypedConstantExpression<string>("test"), new TypedConstantResultExpression<int>(Result.Invalid<int>("Kaboom")), new TypedConstantExpression<int>(1));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new TypedConstantExpression<string>("test"), new TypedConstantExpression<int>(1), new TypedConstantResultExpression<int>(Result.Invalid<int>("Something bad happened")));

        // Act
        var actual = sut.EvaluateTyped();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Something bad happened");
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SubstringExpressionBuilder()
            .WithExpression("AB")
            .WithIndexExpression(1)
            .WithLengthExpression(1)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<SubstringExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SubstringExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(SubstringExpression));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
