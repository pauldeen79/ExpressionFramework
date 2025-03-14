namespace ExpressionFramework.Core.Tests.Evaluatables;

public class ComposedEvaluatableTests : TestBase<ComposableEvaluatableBuilder>
{
    [Fact]
    public void Construction_With_Too_Many_EndGroups_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithEndGroup())
            .Build());

        // Act & Assert
        act.ShouldThrow<ValidationException>().Message.ShouldBe("EndGroup not valid at index 0, because there is no corresponding StartGroup");
    }

    [Fact]
    public void Construction_With_One_Too_Many_StartGroup_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(CreateEvaluatableBuilder().WithStartGroup())
            .Build());

        // Act & Assert
        act.ShouldThrow<ValidationException>().Message.ShouldBe("Missing EndGroup");
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
        act.ShouldThrow<ValidationException>().Message.ShouldBe("2 missing EndGroups");
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
        act.ShouldNotThrow();
    }

    private ComposableEvaluatableBuilder CreateEvaluatableBuilder()
        => CreateSut()
            .WithInnerEvaluatable(
                new OperatorEvaluatableBuilder()
                    .WithLeftValue(null)
                    .WithRightValue(null)
                    .WithOperator(new EqualsOperatorBuilder())
            );
}
