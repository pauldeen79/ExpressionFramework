namespace ExpressionFramework.Core.Tests.Functions.Operators;

public class NotEqualsOperatorTests : TestBase<NotEqualsOperator>
{
    public class Evaluate : NotEqualsOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(1, 2, StringComparison.InvariantCulture);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
