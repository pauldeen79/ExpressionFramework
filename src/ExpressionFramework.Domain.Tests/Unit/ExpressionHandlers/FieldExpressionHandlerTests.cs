namespace ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers;

public class FieldExpressionHandlerTests
{
    [Fact]
    public async Task Evaluate_Returns_NotSupported_When_Expression_Is_Not_A_FieldExpression()
    {
        // Arrange
        var sut = new FieldExpressionHandler(new ValueProvider());
        var expression = new EmptyExpressionBuilder().Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(default, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public async Task Evaluate_Returns_Success_Result_From_ValueProvider()
    {
        // Arrange
        var sut = new FieldExpressionHandler(new ValueProvider());
        var expression = new FieldExpressionBuilder()
            .WithFieldName(nameof(MyClass.MyProperty))
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(new MyClass { MyProperty = "Test" }, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("Test");
    }

    [Fact]
    public async Task Evaluate_Returns_Success_Error_Result_From_ValueProvider()
    {
        // Arrange
        var sut = new FieldExpressionHandler(new ValueProvider());
        var expression = new FieldExpressionBuilder()
            .WithFieldName("WrongPropertyName")
            .Build();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = await sut.Handle(new MyClass { MyProperty = "Test" }, expression, expressionEvaluatorMock.Object);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Fieldname [WrongPropertyName] is not found on type [ExpressionFramework.Domain.Tests.Unit.ExpressionHandlers.FieldExpressionHandlerTests+MyClass]");
    }

    public class MyClass
    {
        public string? MyProperty { get; set; }
    }
}
