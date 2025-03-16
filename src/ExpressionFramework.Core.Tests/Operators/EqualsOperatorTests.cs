namespace ExpressionFramework.Core.Tests.Operators;

public class EqualsOperatorTests : TestBase<EqualsOperator>
{
    public class Evaluate : EqualsOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result_With_Equals_Operator()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(123, 123, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
