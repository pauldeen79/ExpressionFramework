namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class GroupByExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new GroupByExpressionBuilder()
            .WithExpression(default(IEnumerable)!)
            .WithKeySelectorExpression(new DelegateExpressionBuilder().WithValue(x => x?.ToString()?.Length ?? 0))
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Expression()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantResultExpression<IEnumerable>(Result.Error<IEnumerable>("Kaboom")), new TypedConstantExpression<string>("Kaboom"));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccessfulResult_From_Selector()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "c" }), new ErrorExpression(new TypedConstantExpression<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Grouped_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new GroupByExpression(new TypedConstantExpression<IEnumerable>(new[] { "a", "b", "cc" }), new DelegateExpression(x => x?.ToString()?.Length ?? 0));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        using var provider = new ServiceCollection().AddCsharpExpressionDumper().BuildServiceProvider();
        var dumper = provider.GetRequiredService<ICsharpExpressionDumper>();
        var code = dumper.Dump(result.Value);
        code.ShouldBe(@"new[]
{
    new[]
    {
        @""a"",
        @""b"",
    },
    new[]
    {
        @""cc"",
    },
}");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(GroupByExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(GroupByExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
