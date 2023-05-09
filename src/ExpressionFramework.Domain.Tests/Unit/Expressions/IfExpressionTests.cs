namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class IfExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Error_From_Expression_Evaluation()
    {
        // Arrange
        var sut = new IfExpression(new SingleEvaluatable(new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), new EqualsOperator(), new EmptyExpression()), new EmptyExpression(), null);

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Result_Using_Constant_Expression()
    {
        // Arrange
        var expression = new IfExpressionBuilder()
            .WithCondition(new SingleEvaluatableBuilder().WithLeftExpression("A").WithOperator(new EqualsOperatorBuilder()).WithRightExpression("A"))
            .WithResultExpression("Correct")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Correct");
    }

    [Fact]
    public void Evaluate_Returns_Result_No_Value_Provided()
    {
        // Arrange
        var expression = new IfExpressionBuilder()
            .WithResultExpression(new EmptyExpressionBuilder())
            .WithCondition(new SingleEvaluatableBuilder().WithLeftExpression("A").WithOperator(new EqualsOperatorBuilder()).WithRightExpression("A"))
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Result_Using_DefaultExpression()
    {
        // Arrange
        var expression = new IfExpressionBuilder()
            .WithCondition(new SingleEvaluatableBuilder().WithLeftExpression("A").WithOperator(new NotEqualsOperatorBuilder()).WithRightExpression("A"))
            .WithResultExpression("Correct")
            .WithDefaultExpression("Incorrect")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Incorrect");
    }

    [Fact]
    public void Evaluate_Returns_Result_Using_Delegate_Expression()
    {
        // Arrange
        var expression = new IfExpression(new SingleEvaluatable(_ => "A", new EqualsOperator(), _ => "A"), new DelegateExpression(_ => "Correct"), default);

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Correct");
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new IfExpressionBase(new SingleEvaluatable(new EmptyExpression(), new EqualsOperator(), new EmptyExpression()), new EmptyExpression(), null);

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_NotSupported()
    {
        // Arrange
        var expression = new IfExpression(new SingleEvaluatable(new EmptyExpression(), new EqualsOperator(), new EmptyExpression()), new ConstantExpression("success"), null);

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(IfExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(IfExpression));
        result.Parameters.Should().HaveCount(3);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeFalse();
    }
}
