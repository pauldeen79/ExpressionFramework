namespace ExpressionFramework.Domain.Tests.Unit.Models;

public class ExpressionModelFactoryTests : TestBase
{
    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedConstantExpression()
    {
        // Act
        var result = ExpressionModelFactory.CreateTyped(new TypedConstantExpression<int>(1));

        // Assert
        result.Should().BeOfType<TypedConstantExpressionModel<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedConstantResultExpression()
    {
        // Act
        var result = ExpressionModelFactory.CreateTyped(new TypedConstantResultExpression<int>(Result.Success(1)));

        // Assert
        result.Should().BeOfType<TypedConstantResultExpressionModel<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedDelegateExpression()
    {
        // Act
        var result = ExpressionModelFactory.CreateTyped(new TypedDelegateExpression<int>(_ => 1));

        // Assert
        result.Should().BeOfType<TypedDelegateExpressionModel<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedDelegateResultExpression()
    {
        // Act
        var result = ExpressionModelFactory.CreateTyped(new TypedDelegateResultExpression<int>(_ => Result.Success(1)));

        // Assert
        result.Should().BeOfType<TypedDelegateResultExpressionModel<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_Default_Generated_Expression()
    {
        // Act
        var result = ExpressionModelFactory.CreateTyped(new StringLengthExpressionBuilder().WithExpression("test").BuildTyped());

        // Assert
        result.Should().BeOfType<StringLengthExpressionModel>();
    }

    [Fact]
    public void CreateTyped_Throws_On_Unsupported_Expression()
    {
        // Arrange
        var expression = Fixture.Freeze<ITypedExpression<string>>();

        // Act & Assert
        this.Invoking(_ => ExpressionModelFactory.CreateTyped(expression))
            .Should().Throw<NotSupportedException>();
    }
}
