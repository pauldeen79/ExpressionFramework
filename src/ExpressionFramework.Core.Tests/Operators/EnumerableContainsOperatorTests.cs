namespace ExpressionFramework.Core.Tests.Operators;

public class EnumerableContainsOperatorTests : TestBase<EnumerableContainsOperator>
{
    public class Evaluate : EnumerableContainsOperatorTests
    {
        [Fact]
        public void Returns_Correct_Resul_When_LeftValue_Is_Enumerable()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(new[] { 1, 2, 3 }, 3, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }

        [Fact]
        public void Returns_Invalid_When_LeftValue_Is_Not_Enumerable()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(false, 3, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
            result.ErrorMessage.ShouldBe("Left value is not of type IEnumerable");
        }
    }
}
