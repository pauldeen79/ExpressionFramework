namespace ExpressionFramework.Core.Tests.Functions;

public class LeftFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new LeftFunction(10, functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<LeftFunctionBuilder>();
        var leftFunctionBuilder = (LeftFunctionBuilder)actual;
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
