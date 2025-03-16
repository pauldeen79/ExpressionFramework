namespace ExpressionFramework.Core.Tests.Evaluatables;

public class ConstantResultEvaluatableTests : TestBase<ConstantResultEvaluatableBuilder>
{
    public class Evaluate : ConstantResultEvaluatableTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut().WithResult(Result.Success(true)).BuildTyped();

            // Act
            var result = sut.Evaluate();

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
