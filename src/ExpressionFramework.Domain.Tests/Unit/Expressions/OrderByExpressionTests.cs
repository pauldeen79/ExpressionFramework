namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OrderByExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_Null()
    {
        // Arrange
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(default(IEnumerable)!), new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_SortOrders_Is_Empty()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), Enumerable.Empty<ITypedExpression<SortOrder>>());

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Invalid);
        result.ErrorMessage.ShouldBe("SortOrderExpressions should have at least one item");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccesfull_Result_From_SortOrder_Expression()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[] { new SortOrder(new ErrorExpression(new TypedConstantExpression<string>("Kaboom")), SortOrderDirection.Ascending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Expression_Returns_Error()
    {
        // Arrange
        var expression = new OrderByExpression(new TypedConstantResultExpression<IEnumerable>(Result.Error<IEnumerable>("Kaboom")), new[]
        {
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending),
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Ascending)
        }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SortOrderExpression_Returns_Error()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var expression = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), [new TypedConstantResultExpression<SortOrder>(Result.Error<SortOrder>("Kaboom"))]);

        // Act
        var result = expression.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Source_Sequence_When_Empty()
    {
        // Arrange
        var data = Enumerable.Empty<object?>();
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).ShouldBeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Source_Sequence_Sorted_Ascending()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>)!.ToArray().ShouldBeEquivalentTo(new object[] { "A", "B", "C" });
    }

    [Fact]
    public void Evaluate_Returns_Source_Sequence_Sorted_Descending()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Descending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>)!.ToArray().ShouldBeEquivalentTo(new object[] { "C", "B", "A" });
    }

    [Fact]
    public void Evaluate_Can_Sort_Second_Field_Ascending()
    {
        // Arrange
        var data = new[] { "B2", "B1", "C2", "C1", "A2", "A1" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[]
        {
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending),
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Ascending)
        }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate().TryCast<IOrderedEnumerable<object>>();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<IEnumerable<object?>>();
        result.GetValueOrThrow().ToArray().ShouldBeEquivalentTo(new object[] { "C1", "C2", "B1", "B2", "A1", "A2" });
    }

    [Fact]
    public void Evaluate_Can_Sort_Second_Field_Descending()
    {
        // Arrange
        var data = new[] { "B2", "B1", "C2", "C1", "A2", "A1" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[]
        {
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending),
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Descending)
        }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>)!.ToArray().ShouldBeEquivalentTo(new object[] { "C2", "C1", "B2", "B1", "A2", "A1" });
    }

    [Fact]
    public void ToUntyped_Returns_Expression()
    {
        // Arrange
        var data = new[] { "B2", "B1", "C2", "C1", "A2", "A1" };
        var sut = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[]
        {
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending),
            new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Ascending)
        }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var actual = sut.ToUntyped();

        // Assert
        actual.ShouldBeOfType<OrderByExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OrderByExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(OrderByExpression));
        result.Parameters.Count.ShouldBe(2);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }
}
