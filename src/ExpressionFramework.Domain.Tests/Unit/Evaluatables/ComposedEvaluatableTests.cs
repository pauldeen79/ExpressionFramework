namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class ComposedEvaluatableTests
{
    [Fact]
    public void Construction_With_Too_Many_EndGroups_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithEndGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("EndGroup not valid at index 0, because there is no corresponding StartGroup");
    }

    [Fact]
    public void Construction_With_One_Too_Many_StartGroup_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithStartGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("Missing EndGroup");
    }

    [Fact]
    public void Construction_With_Two_Too_Many_StartGroups_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithStartGroup())
            .AddConditions(CreateEvaluatableBuilder().WithStartGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("2 missing EndGroups");
    }

    [Fact]
    public void Construction_With_Right_Number_Of_StartGroups_And_EndGroups_Does_Not_Fail()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithStartGroup())
            .AddConditions(CreateEvaluatableBuilder().WithEndGroup())
            .Build());

        // Act & Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ComposedEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ComposedEvaluatable));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().ContainSingle();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var evaluatable = new ComposedEvaluatableBase(Enumerable.Empty<ComposableEvaluatable>());

        // Act & Assert
        evaluatable.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    private static ComposableEvaluatableBuilder CreateEvaluatableBuilder()
        => new ComposableEvaluatableBuilder()
            .WithLeftExpression(new EmptyExpressionBuilder())
            .WithRightExpression(new EmptyExpressionBuilder())
            .WithOperator(new EqualsOperatorBuilder());
}
