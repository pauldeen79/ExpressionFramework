namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ChainedExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new ErrorExpressionBuilder().WithErrorMessageExpression(new ConstantExpressionBuilder().WithValue("Kaboom")),
                new EmptyExpressionBuilder()
            )
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Sucess_With_Context_As_Value_When_No_Expressions_Are_Provided()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder().Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ChainedExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ChainedExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }
}
