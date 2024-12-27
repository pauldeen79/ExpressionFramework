namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SwitchExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_When_ExpressionEvaluation_Fails()
    {
        // Arrange
        var expression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder()
                .WithCondition(new ConstantEvaluatableBuilder().WithValue(true))
                .WithExpression(new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom"))))
            .Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ConditionEvaluation_Fails_No_Default()
    {
        // Arrange
        var expression = new SwitchExpression([new Case(new ErrorEvaluatable("Kaboom"), new EmptyExpression())], default);

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ConditionEvaluation_Fails_Filled_Default()
    {
        // Arrange
        var expression = new SwitchExpression([new Case(new ErrorEvaluatable("Kaboom"), new EmptyExpression())], null);

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Default_When_No_Cases_Are_Present()
    {
        // Arrange
        var expression = new SwitchExpressionBuilder().Build();

        // Act
        var actual = expression.Evaluate(default);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Success_When_One_Case_Evaluates_To_True()
    {
        // Arrange
        var switchExpression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder()
                .WithExpression(new ContextExpressionBuilder())
                .WithCondition(new SingleEvaluatableBuilder().WithLeftExpression(new ContextExpressionBuilder()).WithOperator(new EqualsOperatorBuilder()).WithRightExpression(new ConstantExpressionBuilder().WithValue("value")))
            )
            .Build();

        // Act
        var result = switchExpression.Evaluate("value");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("value");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Default_Value_When_One_Case_Evaluates_To_False()
    {
        // Arrange
        var switchExpression = new SwitchExpressionBuilder()
            .AddCases(new CaseBuilder()
                .WithExpression(new ContextExpressionBuilder())
                .WithCondition(new SingleEvaluatableBuilder().WithLeftExpression(new ContextExpressionBuilder()).WithOperator(new EqualsOperatorBuilder()).WithRightExpression(new ConstantExpressionBuilder().WithValue("value")))
            )
            .Build();

        // Act
        var result = switchExpression.Evaluate("wrong value");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SwitchExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SwitchExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }

    private sealed record ErrorEvaluatable : Evaluatable
    {
        public ErrorEvaluatable(string errorMessage) => ErrorMessage = errorMessage;

        public string ErrorMessage { get; }

        public override Result<bool> Evaluate(object? context)
            => Result.Error<bool>(ErrorMessage);

        public override EvaluatableBuilder ToBuilder()
        {
            return new ErrorEvaluatableBuilder(this);
        }
    }
    private sealed class ErrorEvaluatableBuilder : EvaluatableBuilder<ErrorEvaluatableBuilder, ErrorEvaluatable>
    {
        public ErrorEvaluatableBuilder(ErrorEvaluatable source) : base(source)
        {
            ArgumentNullException.ThrowIfNull(source);
        }

        public override ErrorEvaluatable BuildTyped()
        {
            return new ErrorEvaluatable(ErrorMessage);
        }

        public string ErrorMessage { get; set; } = "";
    }
}
