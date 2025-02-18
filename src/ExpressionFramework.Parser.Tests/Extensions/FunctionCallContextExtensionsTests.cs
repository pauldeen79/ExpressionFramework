namespace ExpressionFramework.Parser.Tests.Extensions;

public class FunctionCallContextExtensionsTests
{
    private readonly IFunctionEvaluator FunctionEvaluator = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator ExpressionEvaluator = Substitute.For<IExpressionEvaluator>();

    [Fact]
    public void GetArgumentValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue("Some value")).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentValueExpression<string>(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be("Some value");
    }

    [Fact]
    public void GetArgumentValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentValueExpression(0, "Dummy", "Some default value");

        // Assert
        result.EvaluateTyped().Value.Should().Be("Some default value");
    }

    [Fact]
    public void GetArgumentInt32ValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue(13)).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentInt32ValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be(13);
    }

    [Fact]
    public void GetArgumentInt32ValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentInt32ValueExpression(0, "Dummy", 33);

        // Assert
        result.EvaluateTyped().Value.Should().Be(33);
    }

    [Fact]
    public void GetArgumentInt64ValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue(13L)).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentInt64ValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be(13L);
    }

    [Fact]
    public void GetArgumentInt64ValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentInt64ValueExpression(0, "Dummy", 33L);

        // Assert
        result.EvaluateTyped().Value.Should().Be(33L);
    }

    [Fact]
    public void GetArgumentDecimalValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue(4.6M)).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentDecimalValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be(4.6M);
    }

    [Fact]
    public void GetArgumentDecimalValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentDecimalValueExpression(0, "Dummy", 4.9M);

        // Assert
        result.EvaluateTyped().Value.Should().Be(4.9M);
    }

    [Fact]
    public void GetArgumentBooleanValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue(true)).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentBooleanValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be(true);
    }

    [Fact]
    public void GetArgumentBooleanValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentBooleanValueExpression(0, "Dummy", true);

        // Assert
        result.EvaluateTyped().Value.Should().Be(true);
    }

    [Fact]
    public void GetArgumentDateTimeValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var now = DateTime.Now;
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue(now)).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentDateTimeValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be(now);
    }

    [Fact]
    public void GetArgumentDateTimeValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var now = DateTime.Now;
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentDateTimeValueExpression(0, "Dummy", now);

        // Assert
        result.EvaluateTyped().Value.Should().Be(now);
    }

    [Fact]
    public void GetArgumentStringValueExpression_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue("Some value")).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentStringValueExpression(0, "Dummy");

        // Assert
        result.EvaluateTyped().Value.Should().Be("Some value");
    }

    [Fact]
    public void GetArgumentStringValueExpression_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentStringValueExpression(0, "Dummy", "Some default value");

        // Assert
        result.EvaluateTyped().Value.Should().Be("Some default value");
    }

    [Fact]
    public void GetArgumentExpressionResult_No_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").AddArguments(new ConstantArgumentBuilder().WithValue("Some value")).Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentExpressionResult<string>(0, "Dummy");

        // Assert
        result.Value.Should().Be("Some value");
    }

    [Fact]
    public void GetArgumentExpressionResult_Default_Value_Returns_Correct_Result()
    {
        // Arrange
        var sut = new FunctionCallContext(new FunctionCallBuilder().WithName("Dummy").Build(), FunctionEvaluator, ExpressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);

        // Act
        var result = sut.GetArgumentExpressionResult<string>(0, "Dummy", "Some default value");

        // Assert
        result.Value.Should().Be("Some default value");
    }
}
