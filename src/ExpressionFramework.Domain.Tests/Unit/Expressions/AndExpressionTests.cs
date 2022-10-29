namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class AndExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new TrueExpression());

        // Act
        var result = sut.Evaluate("not a boolean");

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context must be of type boolean");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new EmptyExpression());

        // Act
        var result = sut.Evaluate(true);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type boolean");
    }

    [Fact]
    public void Evaluate_Returns_Success_When_Context_And_Expression_Are_Both_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new TrueExpression());

        // Act
        var result = sut.Evaluate(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeEquivalentTo(false);
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new TrueExpression());

        // Act
        var result = sut.EvaluateTyped("not a boolean");

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Context must be of type boolean");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Not_Of_Type_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new EmptyExpression());

        // Act
        var result = sut.EvaluateTyped(true);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("Expression must be of type boolean");
    }

    [Fact]
    public void EvaluateTyped_Returns_Success_When_Context_And_Expression_Are_Both_Boolean()
    {
        // Arrange
        var sut = new AndExpression(new TrueExpression());

        // Act
        var result = sut.EvaluateTyped(false);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be(false);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(AndExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(AndExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
