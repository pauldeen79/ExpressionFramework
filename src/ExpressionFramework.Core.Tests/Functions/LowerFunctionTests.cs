namespace ExpressionFramework.Core.Tests.Functions;

public class LowerFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new LowerFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LowerFunctionBuilder>();
        var lowerFunctionBuilder = (LowerFunctionBuilder)actual;
        if (functionFilled)
        {
            lowerFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            lowerFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
