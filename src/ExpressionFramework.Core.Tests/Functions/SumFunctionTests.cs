namespace ExpressionFramework.Core.Tests.Functions;

public class SumFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new SumFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<SumFunctionBuilder>();
        var sumFunctionBuilder = (SumFunctionBuilder)actual;
        if (functionFilled)
        {
            sumFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            sumFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
