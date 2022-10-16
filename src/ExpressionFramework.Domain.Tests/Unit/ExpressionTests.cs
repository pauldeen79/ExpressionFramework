namespace ExpressionFramework.Domain.Tests.Unit;

public class ExpressionTests
{
    [Fact]
    public void Evaluate_Happy_Flow()
    {
        // Arrange
        var expression = new FieldExpression("Name");
        var context = new { Name = "Hello world!" };

        // Act
        var result = expression.Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world!");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_DuckTyping()
    {
        // Arrange
        var expression = new FieldExpression("InnerProperty.Name");
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var result = expression.Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_ChainedExpression()
    {
        // Arrange
        var expression = new ChainedExpression(new[]
        {
            new FieldExpression("InnerProperty"),
            new FieldExpression("Name")
        });
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var result = expression.Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Add_Multiple_Numbers_Using_ChainedExpression()
    {
        // Arrange
        var expression = new ChainedExpression(new Expression[]
        {
            new CompoundExpression(2, new AddAggregator()),
            new CompoundExpression(3, new AddAggregator())
        });

        // Act
        var result = expression.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(1 + 2 + 3);
    }

    [Fact]
    public void Can_Get_Right_Position_From_String_Using_Substring_And_StringLength()
    {
        // Arrange
        var input = "some string";
        var expression = new ChainedExpressionBuilder().AddExpressions(
            new ContextExpressionBuilder(),
            new SubstringExpressionBuilder()
                .WithIndexExpression(new ChainedExpressionBuilder().AddExpressions(
                    new StringLengthExpressionBuilder(),
                    new CompoundExpressionBuilder().WithAggregator(new SubtractAggregatorBuilder()).WithSecondExpression(new ConstantExpressionBuilder().WithValue(6)))
                )
                .WithLengthExpression(new ConstantExpressionBuilder().WithValue(6))
        ).Build();

        // Act
        var result = expression.Evaluate(input);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("string");
    }

    [Fact]
    public void Can_Concatenate_Multiple_Strings_Using_CompoundExpression()
    {
        // Arrange
        var aggregator = new CompoundExpression(new ConstantExpression("b"), new StringConcatenateAggregator());

        // Act
        var result = aggregator.Evaluate("a");

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("ab");
    }
}
