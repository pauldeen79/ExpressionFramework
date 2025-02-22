using System.Runtime.InteropServices.ObjectiveC;

namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SelectExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new TypedContextExpression<string>(), default));

        // Act
        var result = sut.Evaluate().TryCast<IEnumerable<object>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.GetValueOrThrow().ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void EvaluateTyped_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .BuildTyped();

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void EvaluateTyped_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void EvaluateTyped_Returns_Projected_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new SelectExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ToUpperCaseExpression(new TypedContextExpression<string>(), default));

        // Act
        var result = sut.EvaluateTyped();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value!.ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var sut = new SelectExpressionBuilder()
            .WithExpression(new[] { "a", "b", "c" })
            .WithSelectorExpression(new ToUpperCaseExpressionBuilder().WithExpression(new TypedContextExpressionBuilder<string>()))
            .BuildTyped();

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<SelectExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SelectExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(SelectExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
