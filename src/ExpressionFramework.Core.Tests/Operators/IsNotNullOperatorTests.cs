namespace ExpressionFramework.Core.Tests.Operators;

public class IsNotNullOperatorTests : TestBase<IsNotNullOperator>
{
    public class Evaluate : IsNotNullOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(2, null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
