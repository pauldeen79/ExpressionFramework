﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SubstringExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Substring_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.GetValueOrThrow().Should().BeEquivalentTo("e");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.Evaluate(string.Empty);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.Evaluate(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("not an integer"), new ConstantExpression(1));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new InvalidExpression("Kaboom"));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression("not an integer"));

        // Act
        var actual = sut.Evaluate("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_Substring_From_Context_When_Context_Is_NonEmptyString()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Ok);
        actual.Value.Should().Be("e");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.EvaluateTyped(string.Empty);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.EvaluateTyped(null);

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("not an integer"), new ConstantExpression(1));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new InvalidExpression("Kaboom"));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression("not an integer"));

        // Act
        var actual = sut.EvaluateTyped("test");

        // Assert
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Value_Is_Not_String()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.ValidateContext(null);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Context must be of type string");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_IndexExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("IndexExpression returned an invalid result. Error message: Kaboom");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_IndexExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression("no integer"), new ConstantExpression(1));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("IndexExpression did not return an integer");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Result_Is_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new InvalidExpression("Kaboom"));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("LengthExpression returned an invalid result. Error message: Kaboom");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_LengthExpression_Result_Value_Is_Not_An_Integer()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ConstantExpression("no integer"));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("LengthExpression did not return an integer");
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_IndexExpression_And_LengthExpression_Results_Are_Invalid()
    {
        // Arrange
        var sut = new SubstringExpression(new InvalidExpression("Kaboom"), new InvalidExpression("Kaboom"));

        // Act
        var actual = sut.ValidateContext("test");

        // Assert
        actual.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[]
        {
            "IndexExpression returned an invalid result. Error message: Kaboom",
            "LengthExpression returned an invalid result. Error message: Kaboom"
        });
    }

    [Fact]
    public void ValidateContext_Returns_ValidationError_When_Context_Is_Too_Short()
    {
        // Arrange
        var sut = new SubstringExpression(1, 1);

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Should().ContainSingle();
        actual.Single().ErrorMessage.Should().Be("Index and length must refer to a location within the string");
    }

    [Fact]
    public void ValidateContext_Returns_No_ValidationError_When_LengthExpression_Returns_Error()
    {
        // Arrange
        var sut = new SubstringExpression(new ConstantExpression(1), new ErrorExpression("Kaboom"));

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Should().BeEmpty();
    }
    
    [Fact]
    public void ValidateContext_Returns_No_ValidationError_When_IndexExpression_Returns_Error()
    {
        // Arrange
        var sut = new SubstringExpression(new ErrorExpression("Kaboom"), new ConstantExpression(1));

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_No_ValidationError_When_LengthExpression_And_IndexExpression_Return_Error()
    {
        // Arrange
        var sut = new SubstringExpression(new ErrorExpression("Kaboom"), new ErrorExpression("Kaboom"));

        // Act
        var actual = sut.ValidateContext(string.Empty);

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void ValidateContext_Returns_No_ValidationError_When_All_Is_Well()
    {
        // Arrange
        var sut = new SubstringExpression(0, 1);

        // Act
        var actual = sut.ValidateContext(" ");

        // Assert
        actual.Should().BeEmpty();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SubstringExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SubstringExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextDescription.Should().NotBeEmpty();
        result.ContextTypeName.Should().NotBeEmpty();
        result.ContextIsRequired.Should().BeTrue();
    }
}
