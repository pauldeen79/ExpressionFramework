namespace ExpressionFramework.Core.Tests.Functions;

public class ConditionFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new ConditionFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object)
            .AddConditions(new[] { new ConditionBuilder() }.AsEnumerable());

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<ConditionFunction>();
        var conditionFunction = (ConditionFunction)actual;
        conditionFunction.Conditions.Should().HaveCount(1);
        conditionFunction.InnerFunction.Should().NotBeNull();
    }
}
