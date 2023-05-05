namespace ExpressionFramework.Parser.Tests.FunctionResultParsers;

public class DelegateResultExpressionParserTests
{
    private readonly IFunctionParseResultEvaluator _evaluator;
    private readonly IExpressionParser _parser;
    private readonly FunctionParseResultBuilder _functionParseResultBuilder;

    public DelegateResultExpressionParserTests()
    {
        _evaluator = Mock.Of<IFunctionParseResultEvaluator>();
        Mock.Get(_evaluator)
            .Setup(x => x.Evaluate(It.IsAny<FunctionParseResult>(), It.IsAny<IExpressionParser>(), It.IsAny<object?>()))
            .Returns<FunctionParseResult, IExpressionParser, object?>((result, parser, context) => result.FunctionName == "Context"
                ? Result<object?>.Success(context)
                : Result<object?>.Error("Unknown function"));

        _parser = Mock.Of<IExpressionParser>();
        _functionParseResultBuilder = new FunctionParseResultBuilder()
            .WithContext(CreateContext())
            .WithFunctionName("DelegateResult")
            .WithFormatProvider(CultureInfo.InvariantCulture)
            .AddArguments(new FunctionArgumentBuilder()
                .WithFunction(new FunctionParseResultBuilder()
                    .WithFunctionName("Context")
                    .WithFormatProvider(CultureInfo.InvariantCulture)
                    .WithContext(CreateContext())
                )
            );
    }

    [Fact]
    public void DoParse_ShouldReturnSuccessfulResult_WhenArgumentValueIsSuccessful()
    {
        // Arrange
        _functionParseResultBuilder.WithContext(CreateContext());
        var sut = new DelegateResultExpressionParser();

        // Act
        var result = sut.Parse(_functionParseResultBuilder.Build(), _evaluator, _parser);

        // Assert
        result.Should().BeOfType<Result<Expression>>().Which.IsSuccessful().Should().BeTrue();
    }

    [Fact]
    public void DoParse_ShouldReturnValueOfDelegateResultExpression_WhenArgumentValueIsSuccessful()
    {
        // Arrange
        var sut = new DelegateResultExpressionParser();

        // Act
        var result = sut.Parse(_functionParseResultBuilder.Build(), _evaluator, _parser);

        // Assert
        result.Should().BeOfType<Result<Expression>>()
            .Which.Value.Should().BeOfType<DelegateResultExpression>();
    }

    [Fact]
    public void DoParse_ShouldReturnFailedResult_WhenArgumentValueIsFailed()
    {
        // Arrange
        _functionParseResultBuilder.WithContext("This context value is of the wrong type");
        var sut = new DelegateResultExpressionParser();

        // Act
        var result = sut.Parse(_functionParseResultBuilder.Build(), _evaluator, _parser);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().StartWith("Result is not of type System.Func`2[[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e],[CrossCutting.Common.Results.Result`1[[System.Object, System.Private.CoreLib, Version=7.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], CrossCutting.Common, Version=");
    }

    private static Func<object?, Result<object?>> CreateContext() => new Func<object?, Result<object?>>(_ => Result<object?>.Success("Hello world"));
}
