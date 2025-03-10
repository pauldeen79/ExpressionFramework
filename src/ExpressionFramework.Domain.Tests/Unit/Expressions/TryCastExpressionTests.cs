﻿namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class TryCastExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Success_When_SourceExpression_Can_Be_Cast()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression("Hello world")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("Hello world");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_DefaultExpression_Value_When_SourceExpression_Cannot_Be_Cast_And_DefaultExpression_Is_Specifioed()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .WithDefaultExpression("Hello world")
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo("Hello world");
    }

    [Fact]
    public void Evaluate_Returns_Success_With_Typed_DefaultValue_When_SourceExpression_Cannot_Be_Cast_And_DefaultExpression_Is_Not_Specifioed()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeNull(); // default of IEnumerable is null
    }

    [Fact]
    public void Evaluate_Returns_Failure_When_SourceExpression_Fails()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(new ErrorExpressionBuilder().WithErrorMessageExpression("Kaboom"))
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Failure_When_DefaultExpression_Fails()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .WithDefaultExpression(new TypedConstantResultExpressionBuilder<IEnumerable>().WithValue(Result.Error<IEnumerable>("Kaboom")))
            .Build();

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void ToUntyped_Returns_SourceExpression()
    {
        // Arrange
        var expression = new TryCastExpressionBuilder<IEnumerable>()
            .WithSourceExpression(1)
            .BuildTyped();

        // Act
        var untypedExpression = expression.ToUntyped();

        // Assert
        untypedExpression.ShouldBeOfType<ConstantExpression>().Value.ShouldBe(1);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(TryCastExpression<IEnumerable>));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(TryCastExpression<IEnumerable>));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldBeNull();
        result.ContextTypeName.ShouldBeNull();
        result.ContextIsRequired.ShouldBeNull();
    }
}
