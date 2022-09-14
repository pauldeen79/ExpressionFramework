﻿namespace ExpressionFramework.Domain.Tests.Unit;

public class ConditionEvaluatorTests
{
    [Fact]
    public void Evaluate_Works_Correctly_On_Equals_With_Sequences()
    {
        // Arrange
        var condition = new Condition
        (
            new ConstantExpression(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" })),
            new EqualsOperator(),
            new ConstantExpression(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" }))
        );

        // Act
        var actual = Evaluate(null, new[] { condition });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Evaluate_Works_Correctly_On_Contains_With_Sequence_Of_Strings()
    {
        // Arrange
        var condition = new Condition
        (
            new ConstantExpression(new[] { "1", "2", "3" }),
            new ContainsOperator(),
            new ConstantExpression("2")
        );

        // Act
        var actual = Evaluate(null, new[] { condition });


        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_And_Combination()
    {
        // Arrange
        var condition1 = new Condition
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new Condition
        (
            Combination.And,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321")
        );

        // Act
        var actual = Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Or_Combination()
    {
        // Arrange
        var condition1 = new Condition
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new Condition
        (
            Combination.Or,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("wrong")
        );

        // Act
        var actual = Evaluate(null, new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_1()
    {
        // Arrange
        //This translates to: True&(False|True) -> True
        var condition1 = new Condition
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new Condition
        (
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("wrong"),
            startGroup: true,
            endGroup: false,
            Combination.And
        );
        var condition3 = new Condition
        (
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321"),
            startGroup: false,
            endGroup: true,
            Combination.Or
        );

        // Act
        var actual = Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_2()
    {
        // Arrange
        //This translates to: False|(True&True) -> True
        var condition1 = new Condition
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("wrong")
        );
        var condition2 = new Condition
        (
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321"),
            startGroup: true,
            endGroup: false,
            Combination.Or
        );
        var condition3 = new Condition
        (
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321"),
            startGroup: false,
            endGroup: true,
            Combination.And
        );

        // Act
        var actual = Evaluate(null, new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    private static Result<bool> Evaluate(object? context, IEnumerable<Condition> conditions)
#pragma warning disable CS8605 // Unboxing a possibly null value.
        => Result<bool>.Success((bool)new ConditionalExpression(conditions, new ConstantExpression(true), new ConstantExpression(false)).Evaluate(context).Value);
#pragma warning restore CS8605 // Unboxing a possibly null value.
}
