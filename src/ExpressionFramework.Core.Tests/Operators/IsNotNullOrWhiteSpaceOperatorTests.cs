namespace ExpressionFramework.Core.Tests.Operators;

public class IsNotNullOrWhiteSpaceOperatorTests : TestBase<IsNotNullOrWhiteSpaceOperator>
{
    public class Evaluate : IsNotNullOrWhiteSpaceOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate("2", null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
