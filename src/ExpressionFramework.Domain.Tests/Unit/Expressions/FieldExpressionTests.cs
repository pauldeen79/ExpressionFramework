namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class FieldExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new FieldExpressionBuilder()
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
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
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue("WrongPropertyName"))
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
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
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
            .WithFieldNameExpression(new ConstantExpressionBuilder().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate(new MyClass { MyProperty = null });

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().BeNull();
    }

    [Fact]
    public void ValidateContext_Returns_EmptyResult_When_All_Is_Well()
    {
        // Arrange
        var sut = new FieldExpression(new ConstantExpression(nameof(MyClass.MyProperty)));

        // Act
        var actual = sut.ValidateContext(new MyClass());

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Context_Is_Null()
    {
        // Arrange
        var sut = new FieldExpression(new ConstantExpression("SomeField"));

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
        var sut = new FieldExpression(new ConstantExpression("WrongPropertyName"));

        // Act
        var actual = sut.ValidateContext(new MyClass());

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.Expressions.FieldExpressionTests+MyClass]");
    }

    [Fact]
    public void ValidateContext_Returns_No_ValidationError_On_Nested_Null_Property()
    {
        // Arrange
        var sut = new FieldExpression(new ConstantExpression($"{nameof(MyNestedClass.MyNestedProperty)}.{nameof(MyNestedClass.MyProperty)}"));

        // Act
        var actual = sut.ValidateContext(new MyNestedClass());

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_No_ValidationError_On_Nested_NonNull_Property()
    {
        // Arrange
        var sut = new FieldExpression(new ConstantExpression($"{nameof(MyNestedClass.MyNestedProperty)}.{nameof(MyNestedClass.MyProperty)}"));

        // Act
        var actual = sut.ValidateContext(new MyNestedClass { MyNestedProperty = new MyNestedClass { MyProperty = "Test" } });

        // Assert
        actual.Should().BeEmpty();
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

    public class MyNestedClass
    {
        public MyNestedClass? MyNestedProperty { get; set; }
        public string? MyProperty { get; set; }
    }
}
