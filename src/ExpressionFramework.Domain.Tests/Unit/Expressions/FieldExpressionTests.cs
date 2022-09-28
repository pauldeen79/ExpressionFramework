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
    public void Evaluate_Returns_Invalid_Result_From_ValueProvider()
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
    public void Evaluate_Returns_Ivalid_When_Context_Is_Null()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldName(nameof(MyClass.MyProperty))
            .Build();

        // Act
        var actual = expression.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context cannot be empty");
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

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Context_Is_Null()
    {
        // Arrange
        var sut = new FieldExpression("SomeField");

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context cannot be empty");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_FieldName_Could_Not_Be_Found()
    {
        // Arrange
        var sut = new FieldExpression("WrongPropertyName");

        // Act
        var actual = sut.ValidateContext(new MyClass());

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.Expressions.FieldExpressionTests+MyClass]");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(FieldExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(FieldExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }

    public class MyClass
    {
        public string? MyProperty { get; set; }
    }
}
