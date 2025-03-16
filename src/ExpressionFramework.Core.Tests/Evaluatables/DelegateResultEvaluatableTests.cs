namespace ExpressionFramework.Core.Tests.Evaluatables;

public class DelegateResultEvaluatableTests : TestBase<DelegateResultEvaluatableBuilder>
{
    public class Evaluate : DelegateResultEvaluatableTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut().WithDelegate(_ => Result.Success(true)).BuildTyped();

            // Act
            var result = sut.Evaluate();

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
