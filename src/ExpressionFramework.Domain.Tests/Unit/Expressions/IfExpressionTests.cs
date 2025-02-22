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
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("Correct");
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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeNull();
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
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("Incorrect");
    }

    [Fact]
    public void Evaluate_Returns_Result_Using_Delegate_Expression()
    {
        // Arrange
        var expression = new IfExpression(new SingleEvaluatable(_ => "A", new EqualsOperator(), _ => "A"), new DelegateExpression(_ => "Correct"), default);

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBe("Correct");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(IfExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(IfExpression));
        result.Parameters.Count.ShouldBe(3);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.ContextIsRequired.ShouldBe(false);
    }
}
