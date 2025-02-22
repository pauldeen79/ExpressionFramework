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

    [Fact]
    public void Get_Returns_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ComposedEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ComposedEvaluatable));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.ShouldHaveSingleItem();
    }

    private static ComposableEvaluatableBuilder CreateEvaluatableBuilder()
        => new ComposableEvaluatableBuilder()
            .WithLeftExpression(new EmptyExpressionBuilder())
            .WithRightExpression(new EmptyExpressionBuilder())
            .WithOperator(new EqualsOperatorBuilder());
}
