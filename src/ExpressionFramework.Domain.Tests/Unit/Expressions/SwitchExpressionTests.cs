﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

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
        var expression = new SwitchExpression(new[] { new Case(new ErrorEvaluatable("Kaboom"), new EmptyExpression()) }, default(Func<object?, object?>?));

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
        var expression = new SwitchExpression(new[] { new Case(new ErrorEvaluatable("Kaboom"), new EmptyExpression()) }, _ => null);

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
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new SwitchExpressionBase(Enumerable.Empty<Case>(), null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported_No_Default()
    {
        // Arrange
        var expression = new SwitchExpression(Enumerable.Empty<Case>(), default(object?));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported_Filled_Default()
    {
        // Arrange
        var expression = new SwitchExpression(Enumerable.Empty<Case>(), 12345);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
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
            => Result<bool>.Error(ErrorMessage);
    }
}
