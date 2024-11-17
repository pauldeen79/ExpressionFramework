namespace ExpressionFramework.Parser.Tests.Extensions;

public partial class StringExtensionsTests
{
    public class GetGenericTypeResult
    {
        [Fact]
        public void GetGenericTypeResult_WithValidTypeName_ReturnsType()
        {
            // Arrange
            const string typeName = "System.Nullable<System.Int32>";

            // Act
            var result = typeName.GetGenericTypeResult();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful().Should().BeTrue();
            result.GetValueOrThrow().Should().Be<int>();
        }

        [Fact]
        public void GetGenericTypeResult_WithNoTypeDefined_ReturnsInvalidResult()
        {
            // Arrange
            const string typeName = "";

            // Act
            var result = typeName.GetGenericTypeResult();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful().Should().BeFalse();
            result.ErrorMessage.Should().Be("No type defined");
        }

        [Fact]
        public void GetGenericTypeResult_WithUnknownType_ReturnsInvalidResult()
        {
            // Arrange
            const string typeName = "System.Nullable<MyUndefinedType>";

            // Act
            var result = typeName.GetGenericTypeResult();

            // Assert
            result.Should().NotBeNull();
            result.IsSuccessful().Should().BeFalse();
            result.ErrorMessage.Should().Be("Unknown type: MyUndefinedType");
        }
    }
}
