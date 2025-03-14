namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class OperatorEvaluatableFunctionTests : TestBase<OperatorEvaluatableFunction>
{
    public class Evaluate : OperatorEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_OperatorEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @operator = Fixture.Freeze<IOperator>();
            var functionCall = new FunctionCallBuilder()
                .WithName("OperatorEvaluatable")
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
            result.Value.ShouldBeOfType<OperatorEvaluatable>();
            var operatorEvaluatble = (OperatorEvaluatable)result.Value;
            operatorEvaluatble.Operator.ShouldBeSameAs(@operator);
            operatorEvaluatble.StringComparison.ShouldBe(StringComparison.InvariantCulture);
            operatorEvaluatble.LeftValue.ShouldBeEquivalentTo(1);
            operatorEvaluatble.RightValue.ShouldBeEquivalentTo(2);
        }
    }
}
