namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class ConstantEvaluatableFunctionTests : TestBase<ConstantEvaluatableFunction>
{
    public class Evaluate : ConstantEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_ConstantEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var functionCall = new FunctionCallBuilder()
                .WithName("ConstantEvaluatable")
                .AddArguments(new ConstantArgumentBuilder().WithValue(true));
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<ConstantEvaluatable>();
            var ConstantEvaluatble = (ConstantEvaluatable)result.Value;
            ConstantEvaluatble.Value.ShouldBe(true);
        }
    }
}
