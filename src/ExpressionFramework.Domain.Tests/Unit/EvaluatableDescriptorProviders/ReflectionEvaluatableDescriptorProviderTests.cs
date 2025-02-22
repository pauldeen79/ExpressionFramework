namespace ExpressionFramework.Domain.Tests.Unit.EvaluatableDescriptorProviders;

public class ReflectionEvaluatableDescriptorProviderTests
{
    [Fact]
    public void Throws_On_Null_Type()
    {
        Action a = () => _ = new ReflectionEvaluatableDescriptorProvider(type: null!);
        a.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("type");
    }

    [Fact]
    public void Get_Returns_Default_Values_When_Attributes_Are_Not_Found()
    {
        // Assert
        var sut = new ReflectionEvaluatableDescriptorProvider(GetType());

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldBeEmpty();
        actual.UsesContext.ShouldBeFalse();
        actual.ContextDescription.ShouldBeNull();
        actual.ContextTypeName.ShouldBeNull();
        actual.ContextIsRequired.ShouldBeNull();
        actual.ReturnValues.ShouldBeEmpty();
    }

    [Fact]
    public void Get_Returns_Non_Default_Values_When_Attributes_Are_Found()
    {
        // Assert
        var sut = new ReflectionEvaluatableDescriptorProvider(typeof(SomeEvaluatable));

        // Act
        var actual = sut.Get();

        // Assert
        actual.Description.ShouldNotBeEmpty();
        actual.UsesContext.ShouldBeTrue();
        actual.ContextDescription.ShouldNotBeEmpty();
        actual.ContextTypeName.ShouldNotBeEmpty();
        actual.ContextIsRequired.ShouldBe(true);
        actual.Parameters.ShouldHaveSingleItem();
        actual.Parameters.Single().TypeName.ShouldBe(typeof(string).FullName);
        actual.Parameters.Single().Description.ShouldBe("Some other description");
        actual.Parameters.Single().Name.ShouldBe(nameof(SomeEvaluatable.Parameter));
        actual.ReturnValues.ShouldHaveSingleItem();
        actual.ReturnValues.Single().Description.ShouldBe("Some description");
        actual.ReturnValues.Single().Value.ShouldBe("Some value");
        actual.ReturnValues.Single().Status.ShouldBe(ResultStatus.Ok);
    }

    [EvaluatableDescription("Some description")]
    [UsesContext(true)]
    [ContextDescription("Some description")]
    [ContextType(typeof(string))]
    [ContextRequired(true)]
    [ParameterType(nameof(Parameter), typeof(string))]
    [ParameterRequired(nameof(Parameter), true)]
    [ParameterDescription(nameof(Parameter), "Some other description")]
    [ReturnValue(ResultStatus.Ok, typeof(bool), "Some value", "Some description")]
    private sealed record SomeEvaluatable : Evaluatable
    {
        public override Result<bool> Evaluate(object? context)
            => Result.Success(true);

        public object Parameter { get; } = "";

        public override EvaluatableBuilder ToBuilder()
        {
            return new SomeEvaluatableBuilder(this);
        }
    }

    private sealed class SomeEvaluatableBuilder : EvaluatableBuilder<SomeEvaluatableBuilder, SomeEvaluatable>
    {
        public SomeEvaluatableBuilder(SomeEvaluatable source) : base(source)
        {
            ArgumentNullException.ThrowIfNull(source);
        }

        public override SomeEvaluatable BuildTyped()
        {
            return new SomeEvaluatable();
        }
    }
}
