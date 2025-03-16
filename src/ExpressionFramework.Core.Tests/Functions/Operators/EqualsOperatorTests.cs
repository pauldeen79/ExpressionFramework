namespace ExpressionFramework.Core.Tests.Functions.Operators;

public class EqualsOperatorTests : TestBase<EqualsOperator>
{
    public class Evaluate : EqualsOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(1, 1, StringComparison.InvariantCulture);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
