namespace ExpressionFramework.Domain.Tests.Unit.AggregatorDescriptorProviders;

public class ReflectionAggregatorDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        Action a = () => _ = new ReflectionAggregatorDescriptorProvider(type: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionAggregatorDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldBeEmpty();
        actual.ContextDescription.ShouldBeEmpty();
        actual.ContextTypeName.ShouldBeEmpty();
        actual.Parameters.ShouldBeEmpty();
        actual.ReturnValues.ShouldBeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionAggregatorDescriptorProvider(typeof(SomeAggregator));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldNotBeEmpty();
        actual.ContextDescription.ShouldNotBeEmpty();
        actual.ContextTypeName.ShouldNotBeEmpty();
        actual.Parameters.ShouldHaveSingleItem();
        actual.Parameters.Single().TypeName.ShouldBe(typeof(string).FullName);
        actual.Parameters.Single().Description.ShouldBe("Some other description");
        actual.Parameters.Single().Name.ShouldBe(nameof(SomeAggregator.Parameter));
        actual.ReturnValues.ShouldHaveSingleItem();
        actual.ReturnValues.Single().Description.ShouldBe("Some description");
        actual.ReturnValues.Single().Value.ShouldBe("Some value");
        actual.ReturnValues.Single().Status.ShouldBe(ResultStatus.Ok);
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
            ArgumentNullException.ThrowIfNull(source);
        }

        public override SomeAggregator BuildTyped()
        {
            return new SomeAggregator();
        }
    }
}
