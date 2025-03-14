namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class DelegateResultEvaluatableFunctionTests : TestBase<DelegateResultEvaluatableFunction>
{
    public class Evaluate : DelegateResultEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_DelegateResultEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @delegate = new Func<Result<bool>>(() => Result.Success(true));
            var functionCall = new FunctionCallBuilder()
                .WithName("DelegateResultEvaluatable")
                .AddArguments(new ConstantArgumentBuilder().WithValue(@delegate));
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<DelegateResultEvaluatable>();
            var delegateResultEvaluatable = (DelegateResultEvaluatable)result.Value;
            delegateResultEvaluatable.Delegate.ShouldBeSameAs(@delegate);
        }
    }
}
