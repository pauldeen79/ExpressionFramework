﻿namespace ExpressionFramework.Parser.Tests.Extensions;

public class ExpressionFrameworkParserExtensionsTests : TestBase
{
    private IFunctionParseResultEvaluator Evaluator { get; }
    private IExpressionParser ExpressionParser { get; }

    public ExpressionFrameworkParserExtensionsTests()
    {
        Evaluator = Fixture.Freeze<IFunctionParseResultEvaluator>();
        ExpressionParser = Fixture.Freeze<IExpressionParser>();
    }

    [Fact]
    public void Parse_Returns_Failure_When_Source_Parsing_Fails()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        sut.Parse(Arg.Any<FunctionParseResult>(), Arg.Any<IFunctionParseResultEvaluator>(), Arg.Any<IExpressionParser>())
           .Returns(Result.Error<Expression>("Kaboom"));

        // Act
        var result = sut.Parse<string>(functionParseResult, Evaluator, ExpressionParser);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Parse_Returns_Success_When_Source_Parsing_Succeeds_And_Result_Value_Is_Of_Correct_Type()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        sut.Parse(Arg.Any<FunctionParseResult>(), Arg.Any<IFunctionParseResultEvaluator>(), Arg.Any<IExpressionParser>())
           .Returns(Result.Success<Expression>(new TypedConstantExpression<string>("test")));

        // Act
        var result = sut.Parse<string>(functionParseResult, Evaluator, ExpressionParser);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<TypedConstantExpression<string>>();
    }

    [Fact]
    public void Parse_Returns_Invalid_When_Source_Parsing_Succeeds_And_Result_Value_Is_Not_Of_Correct_Type()
    {
        // Arrange
        var sut = Fixture.Create<IExpressionFrameworkParser>();
        var functionParseResult = new FunctionParseResultBuilder().WithFunctionName("MyFunction").Build();
        sut.Parse(Arg.Any<FunctionParseResult>(), Arg.Any<IFunctionParseResultEvaluator>(), Arg.Any<IExpressionParser>())
           .Returns(Result.Success<Expression>(new TypedConstantExpression<int>(1)));

        // Act
        var result = sut.Parse<string>(functionParseResult, Evaluator, ExpressionParser);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression is not a typed expression of type System.String");
    }
}
