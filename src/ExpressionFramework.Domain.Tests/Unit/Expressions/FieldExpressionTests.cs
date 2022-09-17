namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FieldExpressionTests
{
    [Fact]
    public void Should_Throw_On_Construction_With_Empty_FieldName()
    {
        // Act
        this.Invoking(_ => new FieldExpression(string.Empty))
            .Should().Throw<ValidationException>()
            .WithMessage("The FieldName field is required.");
    }

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
        actual.ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.Expressions.FieldExpressionTests+MyClass]");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_DefaultValue_When_Context_Is_Null()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldName(nameof(MyClass.MyProperty))
            .Build();

        // Act
        var actual = expression.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Success_With_DefaultValue_When_Property_Value_Is_Null()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldName(nameof(MyClass.MyProperty))
            .Build();

        // Act
        var actual = expression.Evaluate(new MyClass { MyProperty = null });

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    public class MyClass
    {
        public string? MyProperty { get; set; }
    }
}
