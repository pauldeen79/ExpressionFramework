namespace ExpressionFramework.Core.Tests.Functions;

public class UpperFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new UpperFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<UpperFunctionBuilder>();
        var upperFunctionBuilder = (UpperFunctionBuilder)actual;
        if (functionFilled)
        {
            upperFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            upperFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
