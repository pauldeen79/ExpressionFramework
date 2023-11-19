namespace ExpressionFramework.Parser.Tests.Extensions;

public partial class StringExtensionsTests
{
    public class GetGenericArguments
    {
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("Foo", "")]
        [InlineData("Foo<Bar>", "Bar")]
        public void GetGenericArguments_ShouldReturnCorrectResult(string? input, string expectedResult)
            => TestGetGenericArguments(input!, expectedResult);

        [Fact]
        public void GetGenericArguments_ShouldReturnEmptyString_WhenInputDoesNotContainOpeningAngleBracket()
            => TestGetGenericArguments("Foo", string.Empty);

        [Fact]
        public void GetGenericArguments_ShouldReturnEmptyString_WhenInputDoesNotContainClosingAngleBracket()
            => TestGetGenericArguments("Foo<Bar", string.Empty);

        [Fact]
        public void GetGenericArguments_ShouldReturnEmptyString_WhenInputContainsOnlyOpeningAngleBracket()
            => TestGetGenericArguments("<", string.Empty);

        [Fact]
        public void GetGenericArguments_ShouldReturnEmptyString_WhenInputContainsOnlyClosingAngleBracket()
            => TestGetGenericArguments(">", string.Empty);

        [Fact]
        public void GetGenericArguments_ShouldReturnEmptyString_WhenInputContainsOpeningAndClosingAngleBracketsButNoTextBetweenThem()
            => TestGetGenericArguments("<>", string.Empty);

        private static void TestGetGenericArguments(string input, string expectedResult)
        {
            // Arrange
            var sut = input;

            // Act
            var result = sut.GetGenericArguments();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
