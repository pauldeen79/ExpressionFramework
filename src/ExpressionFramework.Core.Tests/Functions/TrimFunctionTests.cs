namespace ExpressionFramework.Core.Tests.Functions;

public class TrimFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new TrimFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<TrimFunctionBuilder>();
        var trimFunctionBuilder = (TrimFunctionBuilder)actual;
        if (functionFilled)
        {
            trimFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            trimFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
