namespace ExpressionFramework.Domain.Tests.Unit.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        Action a = () => _ = new ReflectionExpressionDescriptorProvider(type: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionExpressionDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldBeEmpty();
        actual.UsesContext.ShouldBeFalse();
        actual.ContextDescription.ShouldBeNull();
        actual.ContextTypeName.ShouldBeNull();
        actual.ReturnValues.ShouldBeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SomeExpression));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldNotBeEmpty();
        actual.UsesContext.ShouldBeTrue();
        actual.ContextDescription.ShouldNotBeEmpty();
        actual.ContextTypeName.ShouldNotBeEmpty();
        actual.Parameters.ShouldHaveSingleItem();
        actual.Parameters.Single().TypeName.ShouldBe(typeof(string).FullName);
        actual.Parameters.Single().Description.ShouldBe("Some other description");
        actual.Parameters.Single().Name.ShouldBe(nameof(SomeExpression.Parameter));
        actual.ReturnValues.ShouldHaveSingleItem();
        actual.ReturnValues.Single().Description.ShouldBe("Some description");
        actual.ReturnValues.Single().Value.ShouldBe("Some value");
        actual.ReturnValues.Single().Status.ShouldBe(ResultStatus.Ok);
    }

    [ExpressionDescription("Some description")]
    [UsesContext(true)]
    [ContextDescription("Some description")]
    [ContextType(typeof(string))]
    [ParameterType(nameof(Parameter), typeof(string))]
    [ParameterRequired(nameof(Parameter), true)]
    [ParameterDescription(nameof(Parameter), "Some other description")]
    [ReturnValue(ResultStatus.Ok, typeof(object), "Some value", "Some description")]
    private sealed record SomeExpression : Expression
    {
        public override Result<object?> Evaluate(object? context)
            => Result.Success<object?>("some value");

        public object Parameter { get; } = "";

        public override ExpressionBuilder ToBuilder()
        {
            return new SomeExpressionBuilder(this);
        }
    }

    private sealed class SomeExpressionBuilder : ExpressionBuilder<SomeExpressionBuilder, SomeExpression>
    {
        public SomeExpressionBuilder(SomeExpression source) : base(source)
        {
            ArgumentNullException.ThrowIfNull(source);
        }

        public override SomeExpression BuildTyped()
        {
            return new SomeExpression();
        }
    }
}
