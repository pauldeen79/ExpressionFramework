namespace ExpressionFramework.Core.Tests.Functions;

public class ConditionFunctionTests
{
    [Fact]
    public void Can_Create_Builder_With_All_Properties_Filled()
    {
        // Arrange
        var conditionMock = TestFixtures.CreateConditionMock();
        var functionMock = TestFixtures.CreateFunctionMock();
        var sut = new ConditionFunction(new[] { conditionMock.Object }, functionMock.Object);

        // Act
        var actual = sut.ToBuilder();

        // Assert
        actual.Should().BeOfType<ConditionFunctionBuilder>();
        var conditionFunctionBuilder = (ConditionFunctionBuilder)actual;
        conditionFunctionBuilder.InnerFunction.Should().NotBeNull();
        conditionFunctionBuilder.Conditions.Should().ContainSingle();
    }
}
