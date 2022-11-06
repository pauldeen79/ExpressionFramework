namespace ExpressionFramework.Domain.Tests.Unit.Evaluatables;

public class ComposableEvaluatableTests
{
    [Fact]
    public void Evaluate_Works_Correctly_On_Equals_With_Sequences()
    {
        // Arrange
        var condition = new ComposableEvaluatable
        (
            new ConstantExpression(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" })),
            new EqualsOperator(),
            new ConstantExpression(new ReadOnlyValueCollection<string>(new[] { "1", "2", "3" }))
        );

        // Act
        var actual = Evaluate(new[] { condition });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Evaluate_Works_Correctly_On_Contains_With_Sequence_Of_Strings()
    {
        // Arrange
        var condition = new ComposableEvaluatable
        (
            new ConstantExpression(new[] { "1", "2", "3" }),
            new EnumerableContainsOperator(),
            new ConstantExpression("2")
        );

        // Act
        var actual = Evaluate(new[] { condition });


        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Condition_Evaluation_Fails_On_Non_Grouped_Conditions()
    {
        // Arrange
        var condition = new ComposableEvaluatable
        (
            new ConstantExpression(new[] { "1", "2", "3" }),
            new EnumerableContainsOperator(),
            new ErrorExpression(new ConstantExpression("Kaboom"))
        );

        // Act
        var actual = Evaluate(new[] { condition });


        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Condition_Evaluation_Fails_On_Grouped_Conditions()
    {
        // Arrange
        var condition = new ComposableEvaluatable
        (
            true,
            true,
            Combination.And,
            new ConstantExpression(new[] { "1", "2", "3" }),
            new EnumerableContainsOperator(),
            new ErrorExpression(new ConstantExpression("Kaboom"))
        );

        // Act
        var actual = Evaluate(new[] { condition });


        // Assert
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_And_Combination()
    {
        // Arrange
        var condition1 = new ComposableEvaluatable
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new ComposableEvaluatable
        (
            Combination.And,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321")
        );

        // Act
        var actual = Evaluate(new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Or_Combination()
    {
        // Arrange
        var condition1 = new ComposableEvaluatable
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new ComposableEvaluatable
        (
            Combination.Or,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("wrong")
        );

        // Act
        var actual = Evaluate(new[] { condition1, condition2 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_1()
    {
        // Arrange
        //This translates to: True&(False|True) -> True
        var condition1 = new ComposableEvaluatable
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("12345")
        );
        var condition2 = new ComposableEvaluatable
        (
            startGroup: true,
            endGroup: false,
            Combination.And,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("wrong")
        );
        var condition3 = new ComposableEvaluatable
        (
            startGroup: false,
            endGroup: true,
            Combination.Or,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321")
        );

        // Act
        var actual = Evaluate(new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Evaluate_Multiple_Conditions_With_Group_And_Different_Combinations_2()
    {
        // Arrange
        //This translates to: False|(True&True) -> True
        var condition1 = new ComposableEvaluatable
        (
            new ConstantExpression("12345"),
            new EqualsOperator(),
            new ConstantExpression("wrong")
        );
        var condition2 = new ComposableEvaluatable
        (
            startGroup: true,
            endGroup: false,
            Combination.Or,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321")
        );
        var condition3 = new ComposableEvaluatable
        (
            startGroup: false,
            endGroup: true,
            Combination.And,
            new ConstantExpression("54321"),
            new EqualsOperator(),
            new ConstantExpression("54321")
        );

        // Act
        var actual = Evaluate(new[] { condition1, condition2, condition3 });

        // Assert
        actual.GetValueOrThrow().Should().BeTrue();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(ComposableEvaluatable));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(ComposableEvaluatable));
        result.Parameters.Should().HaveCount(6);
        result.ReturnValues.Should().ContainSingle();
    }

    private static Result<bool> Evaluate(IEnumerable<ComposableEvaluatable> conditions)
        => new EvaluatableExpression(new ComposedEvaluatable(conditions)).EvaluateTyped();
}
