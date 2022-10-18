namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionEvaluatableDescriptorProvider(GetType());

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
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(SomeAggregator));

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
        actual.Parameters.Single().Name.Should().Be(nameof(SomeAggregator.Parameter));
        actual.ReturnValues.Should().ContainSingle();
        actual.ReturnValues.Single().Description.Should().Be("Some description");
        actual.ReturnValues.Single().Value.Should().Be("Some value");
        actual.ReturnValues.Single().Status.Should().Be(ResultStatus.Ok);
    }

    [EvaluatableDescription("Some description")]
    [UsesContext(true)]
    [ContextDescription("Some description")]
    [ContextType(typeof(string))]
    [ParameterType(nameof(Parameter), typeof(string))]
    [ParameterRequired(nameof(Parameter), true)]
    [ParameterDescription(nameof(Parameter), "Some other description")]
    [ReturnValue(ResultStatus.Ok, "Some value", "Some description")]
    private sealed record SomeAggregator : Evaluatable
    {
        public override Result<bool> Evaluate(object? context)
            => Result<bool>.Success(true);

        public object Parameter { get; } = "";
    }
}
