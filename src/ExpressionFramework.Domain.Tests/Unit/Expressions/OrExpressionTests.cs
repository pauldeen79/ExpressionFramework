namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OrExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_When_FirstExpression_And_SecondExpression_Are_Both_Successful()
    {
        // Arrange
        var sut = new OrExpressionBuilder()
            .WithFirstExpression(false)
            .WithSecondExpression(true)
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(true);
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_When_FirstExpression_And_SecondExpression_Are_Both_Successful()
    {
        // Arrange
        var sut = new OrExpressionBuilder()
            .WithFirstExpression(false)
            .WithSecondExpression(true)
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(true);
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new OrExpressionBuilder()
            .WithFirstExpression(true)
            .WithSecondExpression(false)
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.Should().BeOfType<OrExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new OrExpressionBase(new TypedConstantExpression<bool>(false), new TypedConstantExpression<bool>(true));

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotSupportedException>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OrExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OrExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }
}
