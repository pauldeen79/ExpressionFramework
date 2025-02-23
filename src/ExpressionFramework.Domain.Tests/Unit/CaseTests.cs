namespace ExpressionFramework.Domain.Tests.Unit;

public class CaseTests
{
    [Fact]
    public void Ctor_Throws_On_Null_Properties()
    {
        // Arrange
        var builder = new CaseBuilder();

        // Act & Assert
        Action a = () => builder.Build();
        a.ShouldThrow<ValidationException>().Message.ShouldBe("The Condition field is required.");
    }

    [Fact]
    public void Can_Validate_Builder_Before_Building_Entity_Instance()
    {
        // Arrange
        var builder = new CaseBuilder();
        var validationResults = new List<ValidationResult>();

        // Act
        _ = builder.TryValidate(validationResults);

        // Assert
        validationResults.Select(x => x.ErrorMessage).ToArray().ShouldBeEquivalentTo(new[] { "The Condition field is required.", "The Expression field is required." });
    }
}
