﻿namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class ComposedEvaluatableTests
{
    [Fact]
    public void Construction_With_Too_Many_EndGroups_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(new SingleEvaluatableBuilder().WithEndGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("EndGroup not valid at index 0, because there is no corresponding StartGroup");
    }

    [Fact]
    public void Construction_With_One_Too_Many_StartGroup_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(new SingleEvaluatableBuilder().WithStartGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("Missing EndGroup");
    }

    [Fact]
    public void Construction_With_Two_Too_Many_StartGroups_Fails()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(new SingleEvaluatableBuilder().WithStartGroup())
            .AddConditions(new SingleEvaluatableBuilder().WithStartGroup())
            .Build());

        // Act & Assert
        act.Should().ThrowExactly<ValidationException>().WithMessage("2 missing EndGroups");
    }

    [Fact]
    public void Construction_With_Right_Number_Of_StartGroups_And_EndGroups_Does_Not_Fail()
    {
        // Arrange
        var act = new Action(() => new ComposedEvaluatableBuilder()
            .AddConditions(new SingleEvaluatableBuilder().WithStartGroup())
            .AddConditions(new SingleEvaluatableBuilder().WithEndGroup())
            .Build());

        // Act & Assert
        act.Should().NotThrow();
    }
}