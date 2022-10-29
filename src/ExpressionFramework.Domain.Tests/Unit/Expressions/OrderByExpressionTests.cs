namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OrderByExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) });

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) });

        // Act
        var result = sut.Evaluate(1);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_SortOrders_Is_Empty()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SortOrderExpressions should have at least one item");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_SortOrders_Is_Of_Wrong_Type()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new[] { new ConstantExpression("no sort order") });

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SortOrderExpressions item with index 0 is not of type SortOrder");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccesfull_Result_From_SortOrder_Expression()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new[] { new SortOrder(new ErrorExpression("Kaboom"), SortOrderDirection.Ascending) });

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccesfull_Result_From_ErrorExpression()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new[] { new InvalidExpression("Kaboom") });

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
        result.ErrorMessage.Should().Be("SortOrderExpressions returned an invalid result on item 0. Error message: Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Source_Sequence_When_Empty()
    {
        // Arrange
        var data = Enumerable.Empty<object?>();
        var sut = new OrderByExpression(new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) });

        // Act
        var result = sut.Evaluate(data);

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
        var sut = new OrderByExpression(new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Ascending) });

        // Act
        var result = sut.Evaluate(data);

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
        var sut = new OrderByExpression(new[] { new SortOrder(new ContextExpression(), SortOrderDirection.Descending) });

        // Act
        var result = sut.Evaluate(data);

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
        var sut = new OrderByExpression(new[]
        {
            new ConstantExpression(new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending)),
            new ConstantExpression(new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Ascending))
        });

        // Act
        var result = sut.Evaluate(data);

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
        var sut = new OrderByExpression(new[]
        {
            new ConstantExpression(new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(0)), SortOrderDirection.Descending)),
            new ConstantExpression(new SortOrder(new DelegateExpression(x => x!.ToString()!.Substring(1)), SortOrderDirection.Descending))
        });

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().BeAssignableTo<IEnumerable<object?>>();
        (result.Value as IEnumerable<object?>).Should().BeEquivalentTo(new[] { "C2", "C1", "B2", "B1", "A2", "A1" });
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
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }
}
