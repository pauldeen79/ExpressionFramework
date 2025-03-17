namespace ExpressionFramework.Core.Tests.Operators;

public class IsNullOperatorTests : TestBase<IsNullOperator>
{
    public class Evaluate : IsNullOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(null, null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
