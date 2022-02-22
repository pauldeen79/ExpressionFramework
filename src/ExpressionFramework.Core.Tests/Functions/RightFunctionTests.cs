namespace ExpressionFramework.Core.Tests.Functions;

public class RightFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new RightFunction(10, functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<RightFunctionBuilder>();
        var leftFunctionBuilder = (RightFunctionBuilder)actual;
        if (functionFilled)
        {
            leftFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            leftFunctionBuilder.InnerFunction.Should().BeNull();
        }
        leftFunctionBuilder.Length.Should().Be(10);
    }
}
