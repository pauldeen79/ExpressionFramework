namespace ExpressionFramework.Core.Tests.Operators;

public class StringContainsOperatorTests : TestBase<StringContainsOperator>
{
    public class Evaluate : StringContainsOperatorTests
    {
        [Fact]
        public void Returns_Invalid_When_Left_Value_Is_Not_Of_Type_String()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate("string", 12, StringComparison.InvariantCulture);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
        }

        [Fact]
        public void Returns_Invalid_When_Right_Value_Is_Not_Of_Type_String()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(12, "string", StringComparison.InvariantCulture);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
        }

        [Fact]
        public void Returns_Success_When_Left_Value_And_Right_Value_Are_Both_Of_Type_String()
        {
            // Arrange
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate("string", "s", StringComparison.Ordinal);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBe(true);
        }
    }
}
