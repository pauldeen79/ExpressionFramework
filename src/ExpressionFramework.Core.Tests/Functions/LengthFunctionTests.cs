namespace ExpressionFramework.Core.Tests.Functions;

public class LengthFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new LengthFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LengthFunctionBuilder>();
        var lengthFunctionBuilder = (LengthFunctionBuilder)actual;
        if (functionFilled)
        {
            lengthFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            lengthFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
