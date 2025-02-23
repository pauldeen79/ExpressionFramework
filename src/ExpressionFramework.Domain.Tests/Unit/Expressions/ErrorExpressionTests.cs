namespace ExpressionFramework.Domain.Tests.Unit.Expressions;

public class ErrorExpressionTests
{
    [Fact]
    public void Evaluate_Returns_ErrorResult_With_Specified_ErrorMessage()
    {
        // Assert
        var sut = new ErrorExpressionBuilder().WithErrorMessageExpression("Error message").Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Error message");
    }

    [Fact]
    public void Evaluate_Returns_ErrorResult_With_Null_ErrorMessage()
    {
        // Assert
        var sut = new ErrorExpressionBuilder().Build();

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBeEmpty();
    }

    [Fact]
    public void Evaluate_Returns_Error_When_ErrorMessageExpression_Returns_Error()
    {
        // Assert
        var sut = new ErrorExpression(new TypedDelegateResultExpression<string>(_ => Result.Error<string>("Kaboom")));

        // Act
        var result = sut.Evaluate();

        // Assert
        result.Status.ShouldBe(ResultStatus.Error);
        result.ErrorMessage.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Determine_Descriptor_Provider()
    {
        // Arrange
        var sut = new ReflectionExpressionDescriptorProvider(typeof(ErrorExpression));

        // Act
        var result = sut.Get();

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(nameof(ErrorExpression));
        result.Parameters.ShouldHaveSingleItem();
        result.ReturnValues.Count.ShouldBe(2);
        result.ContextIsRequired.ShouldBeNull();
    }

    [Fact]
    public void Can_Build_ErrorExpression_From_Builder_With_Default_Message()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder();

        // Act
        var expression = builder.BuildTyped();

        // Assert
        expression.ShouldBeOfType<ErrorExpression>();
        expression.ErrorMessageExpression.ShouldBeOfType<TypedConstantExpression<string>>();
        ((TypedConstantExpression<string>)expression.ErrorMessageExpression).Value.ShouldBeEmpty();
    }

    [Fact]
    public void Cannot_Set_ErrorExpression_To_Null()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder();

        // Act & Assert
        Action a = () => builder.WithErrorMessageExpression(default(ITypedExpressionBuilder<string>)!);
        a.ShouldThrow<ArgumentNullException>();
    }

    [Fact]
    public void Can_Build_ErrorExpression_From_Builder_With_Custom_Message()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder()
            .WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom"));

        // Act
        var expression = builder.BuildTyped();

        // Assert
        expression.ShouldBeOfType<ErrorExpression>();
        expression.ErrorMessageExpression.ShouldBeOfType<TypedConstantExpression<string>>();
        ((TypedConstantExpression<string>)expression.ErrorMessageExpression).Value.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Create_ErrorExpressionBuilder_From_Existing_Constant_ErrorMessage_And_Then_Build_Again()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder
        (
            new ErrorExpressionBuilder()
                .WithErrorMessageExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom"))
                .BuildTyped()
        );

        // Act
        var expression = builder.BuildTyped();

        // Assert
        expression.ShouldBeOfType<ErrorExpression>();
        expression.ErrorMessageExpression.ShouldBeOfType<TypedConstantExpression<string>>();
        ((TypedConstantExpression<string>)expression.ErrorMessageExpression).Value.ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Create_ErrorExpressionBuilder_From_Existing_Delegate_ErrorMessage_And_Then_Build_Again()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder
        (
            new ErrorExpressionBuilder()
                .WithErrorMessageExpression(new TypedDelegateExpressionBuilder<string>().WithValue(_ => "Kaboom"))
                .BuildTyped()
        );

        // Act
        var expression = builder.BuildTyped();

        // Assert
        expression.ShouldBeOfType<ErrorExpression>();
        expression.ErrorMessageExpression.ShouldBeOfType<TypedDelegateExpression<string>>();
        ((TypedDelegateExpression<string>)expression.ErrorMessageExpression).Value.Invoke(default).ShouldBe("Kaboom");
    }

    [Fact]
    public void Can_Create_ErrorExpressionBuilder_From_Existing_LeftExpression_And_Then_Build_Again()
    {
        // Arrange
        var builder = new ErrorExpressionBuilder
        (
            new ErrorExpressionBuilder()
                .WithErrorMessageExpression(new LeftExpressionBuilder().WithExpression(new TypedConstantExpressionBuilder<string>().WithValue("Kaboom")).WithLengthExpression(new TypedConstantExpressionBuilder<int>().WithValue(1)))
                .BuildTyped()
        );

        // Act
        var expression = builder.BuildTyped();

        // Assert
        expression.ShouldBeOfType<ErrorExpression>();
        expression.ErrorMessageExpression.ShouldBeOfType<LeftExpression>();
        ((LeftExpression)expression.ErrorMessageExpression).EvaluateTyped().Value.ShouldBe("K");
    }
}
