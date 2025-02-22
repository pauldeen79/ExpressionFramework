namespace ExpressionFramework.Domain.Tests.Unit.OperatorDescriptorProviders;

public class ReflectionOperatorDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        Action a = () => _ = new ReflectionOperatorDescriptorProvider(type: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionOperatorDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldBeEmpty();
        actual.UsesLeftValue.ShouldBeFalse();
        actual.UsesRightValue.ShouldBeFalse();
        actual.LeftValueTypeName.ShouldBeNull();
        actual.RightValueTypeName.ShouldBeNull();
        actual.ReturnValues.ShouldBeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionOperatorDescriptorProvider(typeof(SomeOperator));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldNotBeEmpty();
        actual.UsesLeftValue.ShouldBeTrue();
        actual.UsesRightValue.ShouldBeTrue();
        actual.LeftValueTypeName.ShouldBe(typeof(string).FullName);
        actual.RightValueTypeName.ShouldBe(typeof(string).FullName);
        actual.Parameters.ShouldHaveSingleItem();
        actual.Parameters.Single().TypeName.ShouldBe(typeof(string).FullName);
        actual.Parameters.Single().Description.ShouldBe("Some other description");
        actual.Parameters.Single().Name.ShouldBe(nameof(SomeOperator.Parameter));
        actual.ReturnValues.ShouldHaveSingleItem();
        actual.ReturnValues.Single().Description.ShouldBe("Some description");
        actual.ReturnValues.Single().Value.ShouldBe("Some value");
        actual.ReturnValues.Single().Status.ShouldBe(ResultStatus.Ok);
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
            => Result.Success(true);

        public object Parameter { get; } = "";

        public override OperatorBuilder ToBuilder()
        {
            return new SomeOperatorBuilder(this);
        }
    }

    private sealed class SomeOperatorBuilder : OperatorBuilder<SomeOperatorBuilder, SomeOperator>
    {
        public SomeOperatorBuilder(SomeOperator source) : base(source)
        {
            ArgumentNullException.ThrowIfNull(source);
        }

        public override SomeOperator BuildTyped()
        {
            return new SomeOperator();
        }
    }
}
