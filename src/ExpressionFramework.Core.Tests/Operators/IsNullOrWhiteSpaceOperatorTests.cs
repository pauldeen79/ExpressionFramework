namespace ExpressionFramework.Core.Tests.Operators;

public class IsNullOrWhiteSpaceOperatorTests : TestBase<IsNullOrWhiteSpaceOperator>
{
    public class Evaluate : IsNullOrWhiteSpaceOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate("   ", null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
