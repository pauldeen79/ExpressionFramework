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
        result.Status.Should().Be(ResultStatus.Invalid);
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
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SortOrderExpressions should have at least one item");
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
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEmpty();
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEquivalentTo(new[] { "A", "B", "C" });
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEquivalentTo(new[] { "C", "B", "A" });
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
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEquivalentTo(new[] { "C1", "C2", "B1", "B2", "A1", "A2" });
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
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEquivalentTo(new[] { "C2", "C1", "B2", "B1", "A2", "A1" });
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
        actual.Should().BeOfType<OrderByExpression>();
    }

    [Fact]
    public void BaseClass_Cannot_Evaluate()
    {
        // Arrange
        var expression = new OrderByExpressionBase(new TypedConstantExpression<IEnumerable>(default(IEnumerable)!), Enumerable.Empty<ITypedExpression<SortOrder>>());

        // Act & Assert
        expression.Invoking(x => x.Evaluate()).Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void GetPrimaryExpression_Returns_Success_With_Expression()
    {
        // Arrange
        var data = Enumerable.Empty<object?>();
        var expression = new OrderByExpression(new TypedConstantExpression<IEnumerable>(data), new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) }.Select(x => new TypedConstantExpression<SortOrder>(x)));

        // Act
        var result = expression.GetPrimaryExpression();

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeOfType<ConstantExpression>();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(OrderByExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(OrderByExpression));
        result.Parameters.Should().HaveCount(2);
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeNull();
    }
}
