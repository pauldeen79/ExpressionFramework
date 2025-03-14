namespace ExpressionFramework.Core.Tests.Functions.Evaluatables;

public class ComposedEvaluatableFunctionTests : TestBase<ComposedEvaluatableFunction>
{
    public class Evaluate : ComposedEvaluatableFunctionTests
    {
        [Fact]
        public void Returns_ComposedEvaluatable_On_Valid_Arguments()
        {
            // Arrange
            var @operator = Fixture.Freeze<IOperator>();
            var functionCall = new FunctionCallBuilder()
                .WithName("ComposedEvaluatable")
                .AddArguments
                (
                    new ConstantArgumentBuilder().WithValue(new ComposableEvaluatable[] { new ComposableEvaluatable(new OperatorEvaluatable(1, @operator, 2, StringComparison.InvariantCulture), null, false, false) })
                );
            var context = CreateFunctionCallContext(functionCall);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeOfType<ComposedEvaluatable>();
            var composedEvaluatble = (ComposedEvaluatable)result.Value;
            composedEvaluatble.Conditions.Count().ShouldBe(1);
            var composableEvaluatable = composedEvaluatble.Conditions.First();
            composableEvaluatable.Combination.ShouldBeNull();
            composableEvaluatable.EndGroup.ShouldBeFalse();
            composableEvaluatable.StartGroup.ShouldBeFalse();

            composableEvaluatable.InnerEvaluatable.ShouldBeOfType<OperatorEvaluatable>();
            var operatorEvaluatable = (OperatorEvaluatable)composableEvaluatable.InnerEvaluatable;
            operatorEvaluatable.Operator.ShouldBeSameAs(@operator);
            operatorEvaluatable.StringComparison.ShouldBe(StringComparison.InvariantCulture);
            operatorEvaluatable.LeftValue.ShouldBeEquivalentTo(1);
            operatorEvaluatable.RightValue.ShouldBeEquivalentTo(2);
        }
    }
}
