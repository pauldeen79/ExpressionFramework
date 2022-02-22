namespace ExpressionFramework.Core.Tests.Functions;

public class ConditionFunctionTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Create_Builder(bool functionFilled)
    {
        // Arrange
        var conditionMock = TestFixtures.CreateConditionMock();
        var functionMock = functionFilled ? TestFixtures.CreateFunctionMock() : null;
        var sut = new ConditionFunction(new[] { conditionMock.Object }, functionMock?.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<ConditionFunctionBuilder>();
        var conditionFunctionBuilder = (ConditionFunctionBuilder)actual;
        if (functionFilled)
        {
            conditionFunctionBuilder.InnerFunction.Should().NotBeNull();
        }
        else
        {
            conditionFunctionBuilder.InnerFunction.Should().BeNull();
        }
        conditionFunctionBuilder.Conditions.Should().ContainSingle();
    }
}
