namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class DelegateEvaluatableFunctionTests : TestBase<DelegateEvaluatableFunction>
{
    public class Evaluate : DelegateEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_DelegateEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var functionCall = new FunctionCallBuilder()
                .WithName("DelegateEvaluatable")
                .AddArguments(new ConstantArgumentBuilder().WithValue(new Func<bool>(() => true)));
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<DelegateEvaluatable>();
            var DelegateEvaluatble = (DelegateEvaluatable)result.Value;
            DelegateEvaluatble.Delegate().ShouldBe(true);
        }
    }
}
