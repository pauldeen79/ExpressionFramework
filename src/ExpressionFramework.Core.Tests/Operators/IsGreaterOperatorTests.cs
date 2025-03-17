namespace ExpressionFramework.Core.Tests.Operators;

public class IsGreaterOperatorTests : TestBase<IsGreaterOperator>
{
    public class Evaluate : IsGreaterOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(2, 1, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
