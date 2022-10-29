namespace ExpressionFramework.Domain.Tests.Unit;

public class ExpressionTests
{
    [Fact]
    public void Evaluate_Happy_Flow()
    {
        // Arrange
        var value = new { Name = "Hello world!" };
        var expression = new FieldExpression(new ConstantExpression(value), new ConstantExpression("Name"));

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world!");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_DuckTyping()
    {
        // Arrange
        var value = new { InnerProperty = new { Name = "Hello world" } };
        var expression = new FieldExpression(new ConstantExpression(value), new ConstantExpression("InnerProperty.Name"));

        // Act
        var result = expression.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world");
    }

    [Fact]
    public void Can_Evaluate_Nested_FieldExpression_Using_ChainedExpression()
    {
        // Arrange
        var value = new { InnerProperty = new { Name = "Hello world" } };
        var expression = new ChainedExpression(new[]
        {
            new FieldExpression(new ConstantExpression(value), new ConstantExpression("InnerProperty")),
            new FieldExpression(new ContextExpression(), new ConstantExpression("Name"))
        });

        // Act
        var result = expression.Evaluate();

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
            new CompoundExpression(1, 2, new AddAggregator()),
            new CompoundExpression(new ContextExpression(), new ConstantExpression(3), new AddAggregator())
        });

        // Act
        var result = expression.Evaluate(null);

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
                    new CompoundExpressionBuilder()
                        .WithAggregator(new SubtractAggregatorBuilder())
                        .WithFirstExpression(new ContextExpressionBuilder())
                        .WithSecondExpression(new ConstantExpressionBuilder().WithValue(6)))
                )
                .WithLengthExpression(new ConstantExpressionBuilder().WithValue(6))
        ).BuildTyped();

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
        var aggregator = new CompoundExpression("a", "b", new StringConcatenateAggregator());

        // Act
        var result = aggregator.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("ab");
    }

    [Fact]
    public void Can_Concatenate_Multiple_Strings_Using_AggregateExpression()
    {
        // Arrange
        var aggregator = new AggregateExpression(new[] { "a", "b", "c" }, new StringConcatenateAggregator());

        // Act
        var result = aggregator.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("abc");
    }

    [Fact]
    public void Can_Get_String_Length_Using_CountExpression()
    {
        // Arrange
        var value = "Hello world!";
        var expression = new CountExpression(new ConstantExpression(value), null);

        // Act
        var result = expression.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world!".Length);
    }

    [Fact]
    public void Can_Get_String_Length_Using_StringLengthExpression()
    {
        // Arrange
        var expression = new StringLengthExpression();
        var context = "Hello world!";

        // Act
        var result = expression.Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world!".Length);
    }
}
