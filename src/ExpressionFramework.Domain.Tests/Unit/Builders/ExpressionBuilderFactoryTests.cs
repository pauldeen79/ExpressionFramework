namespace ExpressionFramework.Domain.Tests.Unit.Builders;

public class ExpressionBuilderFactoryTests
{
    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedConstantExpression()
    {
        // Act
        var result = ExpressionBuilderFactory.CreateTyped(new TypedConstantExpression<int>(1));

        // Assert
        result.Should().BeOfType<TypedConstantExpressionBuilder<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedConstantResultExpression()
    {
        // Act
        var result = ExpressionBuilderFactory.CreateTyped(new TypedConstantResultExpression<int>(Result<int>.Success(1)));

        // Assert
        result.Should().BeOfType<TypedConstantResultExpressionBuilder<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedDelegateExpression()
    {
        // Act
        var result = ExpressionBuilderFactory.CreateTyped(new TypedDelegateExpression<int>(_ => 1));

        // Assert
        result.Should().BeOfType<TypedDelegateExpressionBuilder<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_TypedDelegateResultExpression()
    {
        // Act
        var result = ExpressionBuilderFactory.CreateTyped(new TypedDelegateResultExpression<int>(_ => Result<int>.Success(1)));

        // Assert
        result.Should().BeOfType<TypedDelegateResultExpressionBuilder<int>>();
    }

    [Fact]
    public void CreateTyped_Returns_Correct_Result_On_Default_Generated_Expression()
    {
        // Act
        var result = ExpressionBuilderFactory.CreateTyped(new StringLengthExpressionBuilder().WithExpression("test").BuildTyped());

        // Assert
        result.Should().BeOfType<StringLengthExpressionBuilder>();
    }

    [Fact]
    public void CreateTyped_Throws_On_Unsupported_Expression()
    {
        // Act & Assert
        this.Invoking(_ => ExpressionBuilderFactory.CreateTyped(new MyUnsupportedExpression())).Should().Throw<NotSupportedException>();
    }

    private sealed record MyUnsupportedExpression : Expression, ITypedExpression<int>
    {
        public override Result<object?> Evaluate(object? context)
        {
            throw new NotImplementedException();
        }

        public Result<int> EvaluateTyped(object? context)
        {
            throw new NotImplementedException();
        }

        public Expression ToUntyped()
        {
            throw new NotImplementedException();
        }
    }
}
