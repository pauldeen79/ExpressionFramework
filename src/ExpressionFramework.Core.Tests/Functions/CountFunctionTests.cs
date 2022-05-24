namespace ExpressionFramework.Core.Tests.Functions;

public class CountFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new CountFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<CountFunctionBuilder>();
        var countFunctionBuilder = (CountFunctionBuilder)actual;
        if (functionFilled)
        {
            countFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            countFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
