namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TryCastExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_When_SourceExpression_Can_Be_Cast()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression("Hello world")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_DefaultExpression_Value_When_SourceExpression_Cannot_Be_Cast_And_DefaultExpression_Is_Specifioed()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .WithDefaultExpression("Hello world")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("Hello world");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Typed_DefaultValue_When_SourceExpression_Cannot_Be_Cast_And_DefaultExpression_Is_Not_Specifioed()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull(); // default of IEnumerable is null
    }

    [Fact]
    public void Evaluate_Returns_Failure_When_SourceExpression_Fails()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(new ErrorExpressionBuilder().WithErrorMessageExpression("Kaboom"))
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Failure_When_DefaultExpression_Fails()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .WithDefaultExpression(new TypedConstantResultExpressionBuilder<IEnumerable>().WithValue(Result<IEnumerable>.Error("Kaboom")))
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ToUntyped_Returns_SourceExpression()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .BuildTyped();

        // Act
        var untypedExpression = expression.ToUntyped();

        // Assert
        untypedExpression.Should().BeOfType<ConstantExpression>().Which.Value.Should().Be(1);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TryCastExpression<IEnumerable>));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(TryCastExpression<IEnumerable>));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new TryCastExpressionBase<IEnumerable>(new EmptyExpression(), default);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }
}
