namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class ConstantResultEvaluatableFunctionTests : TestBase<ConstantResultEvaluatableFunction>
{
    public class Evaluate : ConstantResultEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_ConstantResultEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var functionCall = new FunctionCallBuilder()
                .WithName("ConstantResultEvaluatable")
                .AddArguments(new ConstantArgumentBuilder().WithValue(Result.Success(true)));
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<ConstantResultEvaluatable>();
            var constantResultEvaluatble = (ConstantResultEvaluatable)result.Value;
            constantResultEvaluatble.Result.Status.ShouldBe(ResultStatus.Ok);
            constantResultEvaluatble.Result.Value.ShouldBe(true);
        }
    }
}
