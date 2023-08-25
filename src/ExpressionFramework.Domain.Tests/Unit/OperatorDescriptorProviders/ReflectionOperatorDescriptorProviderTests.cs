namespace ExpressionFramework.Domain.Tests.Unit.OperatorDescriptorProviders;

public class ReflectionOperatorDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        this.Invoking(_ => new ReflectionOperatorDescriptorProvider(type: null!))
            .Should().Throw<ArgumentNullException>().WithParameterName("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionOperatorDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().BeEmpty();
        actual.UsesLeftValue.Should().BeFalse();
        actual.UsesRightValue.Should().BeFalse();
        actual.LeftValueTypeName.Should().BeNull();
        actual.RightValueTypeName.Should().BeNull();
        actual.ReturnValues.Should().BeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionOperatorDescriptorProvider(typeof(SomeOperator));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.Should().NotBeEmpty();
        actual.UsesLeftValue.Should().BeTrue();
        actual.UsesRightValue.Should().BeTrue();
        actual.LeftValueTypeName.Should().Be(typeof(string).FullName);
        actual.RightValueTypeName.Should().Be(typeof(string).FullName);
        actual.Parameters.Should().ContainSingle();
        actual.Parameters.Single().TypeName.Should().Be(typeof(string).FullName);
        actual.Parameters.Single().Description.Should().Be("Some other description");
        actual.Parameters.Single().Name.Should().Be(nameof(SomeOperator.Parameter));
        actual.ReturnValues.Should().ContainSingle();
        actual.ReturnValues.Single().Description.Should().Be("Some description");
        actual.ReturnValues.Single().Value.Should().Be("Some value");
        actual.ReturnValues.Single().Status.Should().Be(ResultStatus.Ok);
    }

    [OperatorDescription("Some description")]
    [ParameterType(nameof(Parameter), typeof(string))]
    [ParameterRequired(nameof(Parameter), true)]
    [ParameterDescription(nameof(Parameter), "Some other description")]
    [OperatorUsesLeftValue(true)]
    [OperatorUsesRightValue(true)]
    [OperatorLeftValueType(typeof(string))]
    [OperatorRightValueType(typeof(string))]
    [ReturnValue(ResultStatus.Ok, typeof(bool), "Some value", "Some description")]
    private sealed record SomeOperator : Operator
    {
        protected override Result<bool> Evaluate(object? leftValue, object? rightValue)
            => Result<bool>.Success(true);

        public object Parameter { get; } = "";
    }
}
