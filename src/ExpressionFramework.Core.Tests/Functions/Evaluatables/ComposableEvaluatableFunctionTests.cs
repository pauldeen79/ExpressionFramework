namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class ComposableEvaluatableFunctionTests : TestBase<ComposableEvaluatableFunction>
{
    public class Evaluate : ComposableEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_ComposableEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @operator = new EqualsOperatorBuilder().Build();
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
            var composableEvaluatable = (ComposableEvaluatable)result.Value;
            composableEvaluatable.Combination.ShouldBeNull();
            composableEvaluatable.EndGroup.ShouldBeFalse();
            composableEvaluatable.StartGroup.ShouldBeFalse();

            composableEvaluatable.InnerEvaluatable.ShouldBeOfType<OperatorEvaluatable>();
            var operatorEvaluatable = (OperatorEvaluatable)composableEvaluatable.InnerEvaluatable;
            operatorEvaluatable.Operator.ShouldBeEquivalentTo(@operator);
            operatorEvaluatable.StringComparison.ShouldBe(StringComparison.InvariantCulture);
            operatorEvaluatable.LeftValue.ShouldBeEquivalentTo(1);
            operatorEvaluatable.RightValue.ShouldBeEquivalentTo(2);
        }
    }
}
