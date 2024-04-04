namespace ExpressionFramework.Domain.Tests.Unit.AggregatorDescriptorProviders;

public class ReflectionAggregatorDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        this.Invoking(_ => new ReflectionAggregatorDescriptorProvider(type: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionAggregatorDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().BeEmpty();
        actual.ContextDescription.Should().BeEmpty();
        actual.ContextTypeName.Should().BeEmpty();
        actual.Parameters.Should().BeEmpty();
        actual.ReturnValues.Should().BeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(SomeAggregator));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().NotBeEmpty();
        actual.ContextDescription.Should().NotBeEmpty();
        actual.ContextTypeName.Should().NotBeEmpty();
        actual.Parameters.Should().ContainSingle();
        actual.Parameters.Single().TypeName.Should().Be(typeof(string).FullName);
        actual.Parameters.Single().Description.Should().Be("Some other description");
        actual.Parameters.Single().Name.Should().Be(nameof(SomeAggregator.Parameter));
        actual.ReturnValues.Should().ContainSingle();
        actual.ReturnValues.Single().Description.Should().Be("Some description");
        actual.ReturnValues.Single().Value.Should().Be("Some value");
        actual.ReturnValues.Single().Status.Should().Be(ResultStatus.Ok);
    }

    [AggregatorDescription("Some description")]
    [ContextDescription("Some description")]
    [ContextType(typeof(string))]
    [ParameterType(nameof(Parameter), typeof(string))]
    [ParameterRequired(nameof(Parameter), true)]
    [ParameterDescription(nameof(Parameter), "Some other description")]
    [ReturnValue(ResultStatus.Ok, typeof(object), "Some value", "Some description")]
    private sealed record SomeAggregator : Aggregator
    {
        public override Result<object?> Aggregate(object? context, Expression firstExpression, Expression secondExpression, ITypedExpression<IFormatProvider>? formatProviderExpression)
            => Result.Success<object?>("some value");

        public override AggregatorBuilder ToBuilder()
        {
            return new SomeAggregatorBuilder(this);
        }

        public object Parameter { get; } = "";
    }

    private sealed class SomeAggregatorBuilder : AggregatorBuilder<SomeAggregatorBuilder, SomeAggregator>
    {
        public SomeAggregatorBuilder(SomeAggregator source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
        }

        public override SomeAggregator BuildTyped()
        {
            return new SomeAggregator();
        }
    }
}
