namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class SingleEvaluatableFunctionTests : TestBase<SingleEvaluatableFunction>
{
    public class Evaluate : SingleEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_SingleEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @operator = Fixture.Freeze<IOperator>();
            var functionCall = new FunctionCallBuilder()
                .WithName("SingleEvaluatable")
                .AddArguments
                (
                    new ConstantArgumentBuilder().WithValue(1),
                    new ConstantArgumentBuilder().WithValue(@operator),
                    new ConstantArgumentBuilder().WithValue(2)
                );
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<SingleEvaluatable>();
            var singleEvaluatble = (SingleEvaluatable)result.Value;
            singleEvaluatble.Operator.ShouldBeSameAs(@operator);
            singleEvaluatble.StringComparison.ShouldBe(StringComparison.InvariantCulture);
            singleEvaluatble.LeftValue.ShouldBeEquivalentTo(1);
            singleEvaluatble.RightValue.ShouldBeEquivalentTo(2);
        }
    }
}
