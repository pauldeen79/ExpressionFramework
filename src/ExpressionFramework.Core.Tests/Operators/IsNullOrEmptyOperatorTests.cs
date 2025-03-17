namespace ExpressionFramework.Core.Tests.Operators;

public class IsNullOrEmptyOperatorTests : TestBase<IsNullOrEmptyOperator>
{
    public class Evaluate : IsNullOrEmptyOperatorTests
    {
        [Fact]
        public void Returns_Correct_Result_On_String_That_Is_Not_Empty()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate("2", null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(false);
        }

        [Fact]
        public void Returns_Correct_Result_On_String_That_Is_Empty()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(string.Empty, null, StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }

        [Fact]
        public void Returns_Correct_Result_On_String_That_Is_Null()
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
