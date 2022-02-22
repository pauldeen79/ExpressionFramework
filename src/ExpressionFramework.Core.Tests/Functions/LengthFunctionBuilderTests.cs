namespace ExpressionFramework.Core.Tests.Functions;

public class LengthFunctionBuilderTests
{
    [Fact]
    public void Can_Build_Full_Entity()
    {
        // Arrange
        var functionBuilderMock = TestFixtures.CreateFunctionBuilderMock();
        var sut = new LengthFunctionBuilder()
            .WithInnerFunction(functionBuilderMock.Object);

        // Act
        var actual = sut.Build();

        // Assert
        actual.Should().BeOfType<LengthFunction>();
        var lengthFunction = (LengthFunction)actual;
        lengthFunction.InnerFunction.Should().NotBeNull();
    }
}
