namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class CompoundExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Aggregation_Of_FirstExpression_And_SecondExpression_Using_Constant_Values()
    {
        // Arrange
        var sut = new CompoundExpressionBuilder()
            .WithFirstExpression(1)
            .WithSecondExpression(2)
            .WithAggregator(new AddAggregatorBuilder())
            .Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Ok);
        result.Value.ShouldBeEquivalentTo(1 + 2);
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(CompoundExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(CompoundExpression));
        result.Parameters.Count.ShouldBe(4);
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextDescription.ShouldNotBeEmpty();
        result.ContextTypeName.ShouldNotBeEmpty();
        result.UsesContext.ShouldBeTrue();
        result.ContextIsRequired.ShouldBe(false);
    }
}
