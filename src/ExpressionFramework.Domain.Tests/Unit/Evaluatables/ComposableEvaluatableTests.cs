﻿namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class ComposableEvaluatableTests
{
    [Fact]
    public void Evaluate_Works_Correctly_On_Equals_With_Sequences()
    {
        // Arrange
        var evaluatable = new ComposableEvaluatable
        (
            new ReadOnlyValueCollection<string>(["1", "2", "3"]),
            new EqualsOperator(),
            new ReadOnlyValueCollection<string>(["1", "2", "3"])
        );

        // Act
        var actual = Evaluate([evaluatable]);

        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Evaluate_Works_Correctly_On_Contains_With_Sequence_Of_Strings()
    {
        // Arrange
        var evaluatable = new ComposableEvaluatable
        (
            new[] { "1", "2", "3" },
            new EnumerableContainsOperator(),
            "2"
        );

        // Act
        var actual = Evaluate([evaluatable]);


        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Condition_Evaluation_Fails_On_Non_Grouped_Conditions()
    {
        // Arrange
        var evaluatable = new ComposableEvaluatable
        (
            new ConstantExpression(new[] { "1", "2", "3" }),
            new EnumerableContainsOperator(),
            new ErrorExpression(new TypedConstantExpression<string>("Kaboom")),
            Combination.And,
            false,
            false
        );

        // Act
        var actual = Evaluate([evaluatable]);

        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Condition_Evaluation_Fails_On_Grouped_Conditions()
    {
        // Arrange
        var evaluatable = new ComposableEvaluatable
        (
            new ConstantExpression(new[] { "1", "2", "3" }),
            new EnumerableContainsOperator(),
            new ErrorExpression(new TypedConstantExpression<string>("Kaboom")),
            Combination.And,
            true,
            true
        );

        // Act
        var actual = Evaluate([evaluatable]);


        // Assert
        actual.Status.ShouldBe(ResultStatus.Error);
        actual.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_And_Combination()
    {
        // Arrange
        var evaluatable1 = new ComposableEvaluatable
        (
            _ => "12345",
            () => new EqualsOperator(),
            _ => "12345"
        );
        var evaluatable2 = new ComposableEvaluatable
        (
            _ => "54321",
            () => new EqualsOperator(),
            _ => "54321",
            combination: Combination.And
        );

        // Act
        var actual = Evaluate([evaluatable1, evaluatable2]);

        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Or_Combination()
    {
        // Arrange
        var evaluatable1 = new ComposableEvaluatable
        (
            _ => "12345",
            () => new EqualsOperator(),
            _ => "12345"
        );
        var evaluatable2 = new ComposableEvaluatable
        (
            "54321",
            new EqualsOperator(),
            "wrong",
            combination: Combination.Or
        );

        // Act
        var actual = Evaluate([evaluatable1, evaluatable2]);

        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_1()
    {
        // Arrange
        //This translates to: True&(False|True) -> True
        var evaluatable1 = new ComposableEvaluatable
        (
            "12345",
            new EqualsOperator(),
            "12345"
        );
        var evaluatable2 = new ComposableEvaluatable
        (
            "54321",
            new EqualsOperator(),
            "wrong",
            startGroup: true,
            endGroup: false,
            combination: Combination.And
        );
        var evaluatable3 = new ComposableEvaluatable
        (
            _ => "54321",
            () => new EqualsOperator(),
            _ => "54321",
            startGroup: false,
            endGroup: true,
            combination: Combination.Or
        );

        // Act
        var actual = Evaluate([evaluatable1, evaluatable2, evaluatable3]);

        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_2()
    {
        // Arrange
        //This translates to: False|(True&True) -> True
        var evaluatable1 = new ComposableEvaluatable
        (
            "12345",
            new EqualsOperator(),
            "wrong"
        );
        var evaluatable2 = new ComposableEvaluatable
        (
            "54321",
            new EqualsOperator(),
            "54321",
            startGroup: true,
            endGroup: false,
            combination: Combination.Or
        );
        var evaluatable3 = new ComposableEvaluatable
        (
            "54321",
            new EqualsOperator(),
            "54321",
            startGroup: false,
            endGroup: true,
            combination: Combination.And
        );

        // Act
        var actual = Evaluate([evaluatable1, evaluatable2, evaluatable3]);

        // Assert
        actual.GetValueOrThrow().ShouldBeTrue();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ComposableEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ComposableEvaluatable));
        result.Parameters.Count.ShouldBe(6);
        result.ReturnValues.ShouldHaveSingleItem();
    }

    [Fact]
    public void Throws_On_Null_Delegate()
    {
        Action a = () => _ = new ComposableEvaluatable(_ => null, @operator: null!, _ => null);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("operator");
    }

    private static Result<bool> Evaluate(IEnumerable<ComposableEvaluatable> conditions)
        => new EvaluatableExpression(new ComposedEvaluatable(conditions), new EmptyExpression()).EvaluateTyped();
}
