namespace ExpressionFramework.Core.Tests.Evaluatables;

public class DelegateEvaluatableTests : TestBase<DelegateEvaluatableBuilder>
{
    public class Evaluate : DelegateEvaluatableTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut().WithDelegate(_ => true).BuildTyped();

            // Act
            var result = sut.Evaluate();

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
