namespace ExpressionFramework.Parser.Tests.ExpressionResultParsers;

public class ExpressionParserBaseTests
{
    private readonly IFunctionEvaluator _functionEvaluatorMock = Substitute.For<IFunctionEvaluator>();
    private readonly IExpressionEvaluator _expressionEvaluatorMock = Substitute.For<IExpressionEvaluator>();
    private readonly Expression _expressionMock = Substitute.For<Expression>();

    public ExpressionParserBaseTests()
    {
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<IExpressionEvaluator>(), Arg.Any<IFormatProvider>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(_expressionMock));
        _expressionEvaluatorMock
            .Evaluate(Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<object?>())
            .Returns(x =>
                int.TryParse(x.ArgAt<string>(0), x.ArgAt<IFormatProvider>(1), out var result)
                ? Result.Success<object?>(result)
                : Result.Success<object?>(x.ArgAt<string>(0)));

        _expressionMock
            .Evaluate(Arg.Any<object?>())
            .Returns(Result.Success<object?>("evaluated value"));
    }

    [Fact]
    public void Evaluate_Without_Context_Throws_On_Null_Context()
    {
        // Act & Assert
        this.Invoking(_ => new MyExpressionParser().Evaluate(context: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("context");
    }

    [Fact]
    public void Evaluate_Without_Context_Returns_Continue_For_Wrong_FunctionName()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Wrong")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = new MyExpressionParser().Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Continue);
    }

    [Fact]
    public void ParseExpression_Without_Context_Returns_Success_With_Expression_As_Value_For_Correct_FunctionName()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = new MyExpressionParser().ParseExpression(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<Expression>();
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Evaluated_Value_For_Correct_FunctionName()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = new MyExpressionParser().Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo("evaluated value");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Null_Value_For_Correct_FunctionName_When_Value_Was_Empty()
    {
        // Arrange
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<IExpressionEvaluator>(), Arg.Any<IFormatProvider>(), Arg.Any<object?>())
            .Returns(Result.Success<object?>(null));
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = new MyExpressionParser().Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeNull();
    }

    [Fact]
    public void Evaluate_Returns_Failure_When_Evaluate_Fails()
    {
        // Arrange
        _functionEvaluatorMock
            .Evaluate(Arg.Any<FunctionCall>(), Arg.Any<IExpressionEvaluator>(), Arg.Any<IFormatProvider>(), Arg.Any<object?>())
            .Returns(Result.Error<object?>("Kaboom"));
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = new MyExpressionParser().Evaluate(context);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void ParseTypedExpression_Throws_On_Null_ExpressionType()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);
        var sut = new MyExpressionParser();

        // Act & Assert
        sut.Invoking(_ => MyExpressionParser.DoParseTypedExpression(expressionType: null!, 0, string.Empty, context))
           .Should().Throw<ArgumentNullException>().WithParameterName("expressionType");
    }

    [Fact]
    public void ParseTypedExpression_Throws_On_Null_Context()
    {
        // Arrange
        var sut = new MyExpressionParser();

        // Act & Assert
        sut.Invoking(_ => MyExpressionParser.DoParseTypedExpression(typeof(string), 0, string.Empty, context: null!))
           .Should().Throw<ArgumentNullException>().WithParameterName("context");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Generic_Type_Is_Missing()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = MyExpressionParser.DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", context);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("No type defined");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Argument_Parsing_Fails()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct<System.String>")
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = MyExpressionParser.DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", context);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Missing argument: name");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Failure_When_Argument_Is_Of_Wrong_Type()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct<System.String>")
            .AddArguments(new ExpressionArgumentBuilder().WithValue("1"))
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = MyExpressionParser.DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", context);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Could not create TypedConstantExpression. Error: Constructor on type 'ExpressionFramework.Domain.Expressions.TypedConstantExpression`1[[System.String, System.Private.CoreLib, Version=9.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]]' not found.");
    }

    [Fact]
    public void ParseTypedExpression_Returns_Success_When_Generic_Type_Is_Present_And_Argument_Is_Of_Correct_Type()
    {
        // Arrange
        var context = new FunctionCallContext(new FunctionCallBuilder()
            .WithName("Correct<System.String>")
            .AddArguments(new ConstantArgumentBuilder().WithValue("string value"))
            .Build(), _functionEvaluatorMock, _expressionEvaluatorMock, CultureInfo.InvariantCulture, null);

        // Act
        var result = MyExpressionParser.DoParseTypedExpression(typeof(TypedConstantExpression<>), 0, "name", context);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [FunctionName("Correct")]
    private sealed class MyExpressionParser : ExpressionParserBase
    {
        public MyExpressionParser() : base("Correct")
        {
        }

        protected override Result<Expression> DoParse(FunctionCallContext context)
            => Result.FromExistingResult<Expression>(context.FunctionEvaluator.Evaluate(context.FunctionCall, context.ExpressionEvaluator));

        public static Result<Expression> DoParseTypedExpression(Type expressionType, int index, string argumentName, FunctionCallContext context)
            => ParseTypedExpression(expressionType, index, argumentName, context);
    }
}
