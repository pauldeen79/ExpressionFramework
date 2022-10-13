namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class OrderByExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Ascending)) });

        // Act
        var result = sut.Evaluate(null);

        // Assert
        result.Status.Should().Be(ResultStatus.Invalid);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Ascending)) });

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
        result.ErrorMessage.Should().Be("SortOrders should have at least one item");
    }

    [Fact]
    public void Evaluate_Returns_NonSuccesfull_Result_From_SortOrder_Expression()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ErrorExpression(new ConstantExpression("Kaboom")), SortOrderDirection.Ascending)) });

        // Act
        var result = sut.Evaluate(data);

        // Assert
        result.Status.Should().Be(ResultStatus.Error);
        result.ErrorMessage.Should().Be("Kaboom");
    }

    [Fact]
    public void Evaluate_Returns_Source_Sequence_When_Empty()
    {
        // Arrange
        var data = Enumerable.Empty<object?>();
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Ascending)) });

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
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Ascending)) });

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
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Descending)) });

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
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Descending)) });

        // Act
        var result = sut.ValidateContext(null);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context cannot be empty" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Descending)) });

        // Act
        var result = sut.ValidateContext(44);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "Context must be of type IEnumerable" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_SortOrder_Expression_Returns_Status_Invalid()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new DelegateResultExpression(x => x is string s && s == "a" ? Result<object?>.Invalid("It's wrong") : Result<object?>.Success(x)), SortOrderDirection.Descending)) });

        // Act
        var result = sut.ValidateContext(new object[] { "a", "b", 1, "c" });

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "SortExpression returned an invalid result on item 0. Error message: It's wrong" });
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_SortOrders_Is_Empty()
    {
        // Arrange
        var data = new[] { "B", "C", "A" };
        var sut = new OrderByExpression(Enumerable.Empty<Expression>());

        // Act
        var result = sut.ValidateContext(data);

        // Assert
        result.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new[] { "SortOrders should have at least one item" });
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        // Arrange
        var sut = new OrderByExpression(new[] { new ConstantExpression(new SortOrder(new ContextExpression(), SortOrderDirection.Descending)) });

        // Act
        var result = sut.ValidateContext(new[] { "a", "b", "c" });

        // Assert
        result.Should().BeEmpty();
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
