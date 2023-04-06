namespace ExpressionFramework.Domain.Tests.Unit;

public class CaseTests
{
    [Fact]
    public void Ctor_Throws_On_Null_Properties()
    {
        // Arrange
        var builder = new CaseBuilder();

        // Act & Assert
        builder.Invoking(x => x.Build()).Should().Throw<ValidationException>().WithMessage("The Condition field is required.");
    }

    [Fact]
    public void Can_Validate_Builder_Before_Building_Entity_Instance()
    {
        // Arrange
        var builder = new CaseBuilder();

        // Act
        var validationResults = builder.Validate(new ValidationContext(builder));

        // Assert
        validationResults.Select(x => x.ErrorMessage).Should().BeEquivalentTo("The Condition field is required.", "The Expression field is required.");
    }
}
