namespace ExpressionFramework.Core.Tests.Functions;

public class MonthFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder_With_All_Properties_Filled(bool functionFilled)
    {
        // Arrange
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new MonthFunction(functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<MonthFunctionBuilder>();
        var monthFunctionBuilder = (MonthFunctionBuilder)actual;
        if (functionFilled)
        {
            monthFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            monthFunctionBuilder.InnerFunction.Should().BeNull();
        }
    }
}
