namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class SumExpressionTests
{
    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Context_Is_Not_Of_Type_Enumerable()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_SelectorExpression_Returns_Error()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_Context_Enumerable_Values_Are_Not_All_Numeric()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Byte_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Short_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Int_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Long_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Float_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Double_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Decimal_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_Values_When_SelectorExpression_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Evaluate_Returns_Sum_Of_SelectorExpression_Result_When_SelectorExpression_Is_Not_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Null()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Is_Not_Of_Type_Enumerable()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void ValidateContext_Returns_Item_When_Context_Enumerable_Values_Are_Not_All_Numeric()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void ValidateContext_Returns_Empty_Sequence_When_All_Is_Well()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SumExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameof(SumExpression));
        result.Parameters.Should().ContainSingle();
        result.ReturnValues.Should().HaveCount(2);
        result.ContextIsRequired.Should().BeTrue();
    }

}
