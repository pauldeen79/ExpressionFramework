namespace ExpressionFramework.Domain.Tests.Unit;

public class ExpresionTests
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
}
