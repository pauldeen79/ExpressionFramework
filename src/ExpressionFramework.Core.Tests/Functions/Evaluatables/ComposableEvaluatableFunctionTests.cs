namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class ComposableEvaluatableFunctionTests : TestBase<ComposableEvaluatableFunction>
{
    public class Evaluate : ComposableEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_ComposableEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @operator = Fixture.Freeze<IOperator>();
            var functionCall = new FunctionCallBuilder()
                .WithName("ComposableEvaluatable")
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
            result.Value.ShouldBeOfType<ComposableEvaluatable>();
            var composableEvaluatble = (ComposableEvaluatable)result.Value;
            composableEvaluatble.Combination.ShouldBeNull();
            composableEvaluatble.EndGroup.ShouldBeFalse();
            composableEvaluatble.Operator.ShouldBeSameAs(@operator);
            composableEvaluatble.StartGroup.ShouldBeFalse();
            composableEvaluatble.StringComparison.ShouldBe(StringComparison.InvariantCulture);
            composableEvaluatble.LeftValue.ShouldBeEquivalentTo(1);
            composableEvaluatble.RightValue.ShouldBeEquivalentTo(2);
        }
    }
}
