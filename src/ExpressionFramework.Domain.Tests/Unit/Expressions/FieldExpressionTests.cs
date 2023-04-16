namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FieldExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = "Test" }))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("Test");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = "Test" }))
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue("WrongPropertyName"))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.Expressions.FieldExpressionTests+MyClass]");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithExpression(new EmptyExpressionBuilder())
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Expression cannot be empty");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithExpression(new ErrorExpressionBuilder().WithErrorMessageExpression(new ConstantExpressionBuilder().WithValue("Kaboom")))
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_FieldNameExpression_Returns_Empty_String()
    {
        // Arrange
        var expression = new FieldExpression(new ConstantExpression(new MyClass { MyProperty = "Test" }), new ConstantExpression(string.Empty));

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("FieldNameExpression must be a non empty string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_FieldNameExpression_Returns_Error()
    {
        // Arrange
        var expression = new FieldExpression(new ConstantExpression(new MyClass { MyProperty = "Test" }), new ErrorExpression(new ConstantExpression("Kaboom")));

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_FieldNameExpression_Returns_Non_String_Value()
    {
        // Arrange
        var expression = new FieldExpression(new ConstantExpression(new MyClass { MyProperty = "Test" }), new ConstantExpression(1));

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("FieldNameExpression must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_DefaultValue_When_Property_Value_Is_Null()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = null }))
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new FieldExpressionBase(new EmptyExpression(), new EmptyExpression());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var expression = new FieldExpression(new ConstantExpression(new MyClass()), new ConstantExpression(nameof(MyClass.MyProperty)));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
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
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeNull();
    }

    public class MyClass
    {
        public string? MyProperty { get; set; }
    }

    public class MyNestedClass
    {
        public MyNestedClass? MyNestedProperty { get; set; }
        public string? MyProperty { get; set; }
    }
}
