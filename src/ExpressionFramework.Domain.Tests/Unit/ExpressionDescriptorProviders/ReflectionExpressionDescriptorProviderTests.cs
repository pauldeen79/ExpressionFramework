namespace ExpressionFramework.Domain.Tests.Unit.ExpressionDescriptorProviders;

public class ReflectionExpressionDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        this.Invoking(_ => new ReflectionExpressionDescriptorProvider(type: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionExpressionDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().BeEmpty();
        actual.UsesContext.Should().BeFalse();
        actual.ContextDescription.Should().BeNull();
        actual.ContextTypeName.Should().BeNull();
        actual.ReturnValues.Should().BeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionExpressionDescriptorProvider(typeof(SomeExpression));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().NotBeEmpty();
        actual.UsesContext.Should().BeTrue();
        actual.ContextDescription.Should().NotBeEmpty();
        actual.ContextTypeName.Should().NotBeEmpty();
        actual.Parameters.Should().ContainSingle();
        actual.Parameters.Single().TypeName.Should().Be(typeof(string).FullName);
        actual.Parameters.Single().Description.Should().Be("Some other description");
        actual.Parameters.Single().Name.Should().Be(nameof(SomeExpression.Parameter));
        actual.ReturnValues.Should().ContainSingle();
        actual.ReturnValues.Single().Description.Should().Be("Some description");
        actual.ReturnValues.Single().Value.Should().Be("Some value");
        actual.ReturnValues.Single().Status.Should().Be(ResultStatus.Ok);
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
