namespace ExpressionFramework.Core.Tests.Functions.Operators;

public sealed class OperatorFunctionTests : IDisposable
{
    private readonly ServiceProvider _serviceProvider;
    private readonly IServiceScope _scope;

    public OperatorFunctionTests()
    {
        _serviceProvider = new ServiceCollection()
            .AddParsers()
            .AddExpressionFramework()
            .BuildServiceProvider();

        _scope = _serviceProvider.CreateScope();
    }

    public void Dispose()
    {
        _scope.Dispose();
        _serviceProvider.Dispose();
    }

    [Theory]
    [InlineData(typeof(EndsWithOperatorFunction), typeof(EndsWithOperator))]
    [InlineData(typeof(EnumerableContainsOperatorFunction), typeof(EnumerableContainsOperator))]
    [InlineData(typeof(EnumerableNotContainsOperatorFunction), typeof(EnumerableNotContainsOperator))]
    [InlineData(typeof(EqualsOperatorFunction), typeof(EqualsOperator))]
    [InlineData(typeof(IsGreaterOperatorFunction), typeof(IsGreaterOperator))]
    [InlineData(typeof(IsGreaterOrEqualOperatorFunction), typeof(IsGreaterOrEqualOperator))]
    [InlineData(typeof(IsNotNullOperatorFunction), typeof(IsNotNullOperator))]
    [InlineData(typeof(IsNotNullOrEmptyOperatorFunction), typeof(IsNotNullOrEmptyOperator))]
    [InlineData(typeof(IsNotNullOrWhiteSpaceOperatorFunction), typeof(IsNotNullOrWhiteSpaceOperator))]
    [InlineData(typeof(IsNullOperatorFunction), typeof(IsNullOperator))]
    [InlineData(typeof(IsNullOrEmptyOperatorFunction), typeof(IsNullOrEmptyOperator))]
    [InlineData(typeof(IsNullOrWhiteSpaceOperatorFunction), typeof(IsNullOrWhiteSpaceOperator))]
    [InlineData(typeof(IsSmallerOperatorFunction), typeof(IsSmallerOperator))]
    [InlineData(typeof(IsSmallerOrEqualOperatorFunction), typeof(IsSmallerOrEqualOperator))]
    [InlineData(typeof(NotEndsWithOperatorFunction), typeof(NotEndsWithOperator))]
    [InlineData(typeof(NotEqualsOperatorFunction), typeof(NotEqualsOperator))]
    [InlineData(typeof(NotStartsWithOperatorFunction), typeof(NotStartsWithOperator))]
    [InlineData(typeof(StartsWithOperatorFunction), typeof(StartsWithOperator))]
    [InlineData(typeof(StringContainsOperatorFunction), typeof(StringContainsOperator))]
    [InlineData(typeof(StringNotContainsOperatorFunction), typeof(StringNotContainsOperator))]
    public void Evaluate_Returns_Correct_Result(Type functionType, Type expectedResultType)
    {
        // Arrange
        var sut = _scope.ServiceProvider.GetServices<IFunction>().First(x => x.GetType() == functionType);
        var functionCall = new FunctionCallBuilder().WithName("Dummy");
        var functionEvaluator = _scope.ServiceProvider.GetRequiredService<IFunctionEvaluator>();
        var expressionEvaluator = _scope.ServiceProvider.GetRequiredService<IExpressionEvaluator>();

        // Act
        var result = sut.Evaluate(new FunctionCallContext(functionCall, functionEvaluator, expressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null));

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeOfType(expectedResultType);
    }
}
