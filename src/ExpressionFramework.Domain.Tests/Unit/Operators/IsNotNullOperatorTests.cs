namespace ExpressionFramework.Domain.Tests.Unit.Operators;

public class IsNotNullOperatorTests
{
    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionOperatorDescriptorProvider(typeof(IsNotNullOperator));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(IsNotNullOperator));
        result.Parameters.ShouldBeEmpty();
        result.UsesLeftValue.ShouldBeTrue();
        result.LeftValueTypeName.ShouldNotBeEmpty();
        result.UsesRightValue.ShouldBeFalse();
        result.RightValueTypeName.ShouldBeNull();
        result.ReturnValues.ShouldHaveSingleItem();
    }
}
