namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TypedFieldExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<string>()
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue(nameof(MyClass.MyProperty)))
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = "Test" }))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Ok);
        actual.Value.ShouldBe("Test");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_Result_From_ValueProvider()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<string>()
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = "Test" }))
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue("WrongPropertyName"))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.Expressions.TypedFieldExpressionTests+MyClass]");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<string>()
            .WithExpression(new EmptyExpressionBuilder())
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Expression cannot be empty");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Result_Is_Of_Wrong_Type()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<int>()
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = "Test" }))
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Field is not of type [System.Int32]");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<string>()
            .WithExpression(new ErrorExpressionBuilder().WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom")))
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_FieldNameExpression_Returns_Empty_String()
    {
        // Arrange
        var expression = new TypedFieldExpression<string>(new ConstantExpression(new MyClass { MyProperty = "Test" }), new TypedConstantExpression<string>(string.Empty));

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("FieldNameExpression must be a non empty string");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_FieldNameExpression_Returns_Error()
    {
        // Arrange
        var expression = new TypedFieldExpression<string>(new ConstantExpression(new MyClass { MyProperty = "Test" }), new TypedConstantResultExpression<string>(Result.Error<string>("Kaboom")));

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Property_Value_Is_Null()
    {
        // Arrange
        var expression = new TypedFieldExpressionBuilder<string>()
            .WithExpression(new ConstantExpressionBuilder().WithValue(new MyClass { MyProperty = null }))
            .WithFieldNameExpression(new TypedConstantExpressionBuilder<string>().WithValue(nameof(MyClass.MyProperty)))
            .Build();

        // Act
        var actual = expression.Evaluate();

        // Assert
        actual.Status.ShouldBe(ResultStatus.Invalid);
        actual.ErrorMessage.ShouldBe("Field is not of type [System.String]");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TypedFieldExpression<string>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TypedFieldExpression<string>));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
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
