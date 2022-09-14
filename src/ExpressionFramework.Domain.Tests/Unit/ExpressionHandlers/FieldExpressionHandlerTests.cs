namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class FieldExpressionHandlerTests
{
    [Fact]
    public void Evaluate_Returns_Success_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldName(nameof(MyClass.MyProperty))
            .Build();

        // Act
        var actual = expression.Evaluate(new MyClass { MyProperty = "Test" });

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("Test");
    }

    [Fact]
    public void Evaluate_Returns_Success_Error_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldName("WrongPropertyName")
            .Build();

        // Act
        var actual = expression.Evaluate(new MyClass { MyProperty = "Test" });

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers.FieldExpressionHandlerTests+MyClass]");
    }

    public class MyClass
    {
        public string? MyProperty { get; set; }
    }
}
