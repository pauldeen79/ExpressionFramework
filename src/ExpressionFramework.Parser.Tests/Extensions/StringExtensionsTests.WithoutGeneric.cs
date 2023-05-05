namespace ExpressionFramework.Parser.Tests.Extensions;

public partial class StringExtensionsTests
{
    public class WithoutGenerics
    {
        [Theory]
        [InlineData("Hello", "Hello")]
        [InlineData("World", "World")]
        public void WithoutGenerics_ShouldReturnStringWithoutAngleBrackets(string input, string expectedOutput)
            => TestWithoutGenerics(input, expectedOutput);

        [Theory]
        [InlineData("Test<DataType>", "Test")]
        [InlineData("My<Name>Is<John>", "My")]
        [InlineData("<>", "")]
        [InlineData("", "")]
        public void WithoutGenerics_ShouldReturnCorrectOutputWithAngleBrackets(string input, string expectedOutput)
            => TestWithoutGenerics(input, expectedOutput);

        private static void TestWithoutGenerics(string input, string expectedOutput)
        {
            // Arrange
            var sut = input;

            // Act
            var actualOutput = sut.WithoutGenerics();

            // Assert
            actualOutput.Should().Be(expectedOutput);
        }
    }
}
