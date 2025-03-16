namespace ExpressionFramework.Core.Tests.Evaluatables;

public class ConstantEvaluatableTests : TestBase<ConstantEvaluatableBuilder>
{
    public class Evaluate : ConstantEvaluatableTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut().WithValue(true).BuildTyped();

            // Act
            var result = sut.Evaluate();

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
