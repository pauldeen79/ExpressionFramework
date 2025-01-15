namespace ExpressionFramework.Parser.Tests.Extensions;

public class ExpressionFrameworkParserExtensionsTests : TestBase
{
    private IFunctionEvaluator Evaluator { get; }
    private IExpressionEvaluator ExpressionParser { get; }

    public ExpressionFrameworkParserExtensionsTests()
    {
        Evaluator = Fixture.Freeze<IFunctionEvaluator>();
        ExpressionParser = Fixture.Freeze<IExpressionEvaluator>();
    }

    [Fact]
    public void Parse_Returns_Failure_When_Source_Parsing_Fails()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("MyFunction").Build(), Evaluator, ExpressionParser, CultureInfo.InvariantCulture, null);
        sut.ParseExpression(Arg.Any<FunctionCallContext>())
           .Returns(Result.Error<Expression>("Kaboom"));

        // Act
        var result = sut.ParseExpression<string>(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Parse_Returns_Success_When_Source_Parsing_Succeeds_And_Result_Value_Is_Of_Correct_Type()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("MyFunction").Build(), Evaluator, ExpressionParser, CultureInfo.InvariantCulture, null);
        sut.ParseExpression(Arg.Any<FunctionCallContext>())
           .Returns(Result.Success<Expression>(new TypedConstantExpression<string>("test")));

        // Act
        var result = sut.ParseExpression<string>(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [Fact]
    public void Parse_Returns_Invalid_When_Source_Parsing_Succeeds_And_Result_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionCallContext = new FunctionCallContext(new FunctionCallBuilder().WithName("MyFunction").Build(), Evaluator, ExpressionParser, CultureInfo.InvariantCulture, null);
        sut.ParseExpression(Arg.Any<FunctionCallContext>())
           .Returns(Result.Success<Expression>(new TypedConstantExpression<int>(1)));

        // Act
        var result = sut.ParseExpression<string>(functionCallContext);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not a typed expression of type System.String");
    }
}
