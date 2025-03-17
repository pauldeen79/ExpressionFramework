namespace ExpressionFramework.Core.Tests.Operators;

public class IsSmallerOrEqualOperatorTests : TestBase<IsSmallerOrEqualOperator>
{
    public class Evaluate : IsSmallerOrEqualOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(1, 1, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
