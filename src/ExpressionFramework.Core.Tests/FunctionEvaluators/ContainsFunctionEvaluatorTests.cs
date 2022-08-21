namespace ExpressionFramework.Core.Tests.FunctionEvaluators;

public class ContainsFunctionEvaluatorTests
{
    [Fact]
    public void TryParse_Return_False_When_Function_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new ContainsFunctionEvaluator();
        var functionMock = new Mock<IExpressionFunction>();
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(functionMock.Object, "test", null, expressionMock.Object, expressionEvaluatorMock.Object, out var _);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Of_Correct_Type()
    {
        // Arrange
        var sut = new ContainsFunctionEvaluator();
        var value = new List<string> { "1", "2", "3" };
        var function = new ContainsFunction("2", null);
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(function, value, null, expressionMock.Object, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().Be(value.Contains("2"));
    }

    [Fact]
    public void TryParse_Returns_True_When_Function_Is_Of_Correct_Type_And_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = new ContainsFunctionEvaluator();
        var value = 0; //integer, cannot convert this to IEnumerable!
        var function = new ContainsFunction("2", null);
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(function, value, null, expressionMock.Object, expressionEvaluatorMock.Object, out var functionResult);

        // Assert
        actual.Should().BeTrue();
        functionResult.Should().BeNull();
    }
}
