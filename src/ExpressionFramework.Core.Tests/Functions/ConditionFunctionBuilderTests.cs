namespace ExpressionFramework.Core.Tests.Functions;

public class ConditionFunctionBuilderTests
{
    [Theory, InlineData(true), InlineData(false)]
    public void Can_Build_Entity(bool functionFilled)
    {
        // Arrange
        var functionBuilderMock = functionFilled ? TestFixtures.CreateFunctionBuilderMock() : null;
        var sut = new ConditionFunctionBuilder()
            .WithInnerFunction(functionBuilderMock?.Object)
            .AddConditions(new[] { new ConditionBuilder() }.AsEnumerable());

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<ConditionFunction>();
        var conditionFunction = (ConditionFunction)actual;
        conditionFunction.Conditions.Should().HaveCount(1);
        if (functionFilled)
        {
            conditionFunction.InnerFunction.Should().NotBeNull();
        }
        else
        {
            conditionFunction.InnerFunction.Should().BeNull();
        }
    }
}
