namespace ExpressionFramework.Core.Tests.Operators;

public class IsSmallerOperatorTests : TestBase<IsSmallerOperator>
{
    public class Evaluate : IsSmallerOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(1, 2, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
