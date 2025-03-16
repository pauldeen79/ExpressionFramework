namespace ExpressionFramework.Core.Tests.Operators;

public class NotEqualsOperatorTests : TestBase<NotEqualsOperator>
{
    public class Evaluate : NotEqualsOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result_With_Equals_Operator()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(123, 321, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
