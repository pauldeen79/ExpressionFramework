namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProviderTests
{
    [Fact]
    public void Evaluate_Returns_NotSupported_When_Expression_Is_Not_A_CompositeExpression()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Evaluate_Returns_Success_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns(true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1 + 2;
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression) => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Unknown_On_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns(true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>()))
                     .Returns(CompositeFunctionEvaluatorResultBuilder.NotSupported.Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Unknown composite function: [Mock<ICompositeFunction:1>.Object]");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Unknown_On_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<object?>(),
                                               It.IsAny<object?>(),
                                               It.IsAny<IExpressionEvaluator>(),
                                               It.IsAny<IExpression>()))
            .Returns<ICompositeFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
            ((function, isFirstItem, value, context, evaluator, expression)
            => isFirstItem
                ? CompositeFunctionEvaluatorResultBuilder.Supported
                    .WithResult(tempResult)
                    .WithShouldContinue(isFirstItem)
                    .Build()
                : CompositeFunctionEvaluatorResultBuilder.NotSupported.Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("Unknown composite function: [Mock<ICompositeFunction:1>.Object]");
    }

    [Fact]
    public void Evaluate_Returns_Invalid_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known_And_No_Expressions_Are_Provided()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>());
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("No expressions found");
    }

    [Fact]
    public void Evaluate_Should_Not_Continue_When_CompositeFunction_Says_To_Stop_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).WithShouldContinue(false).Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Evaluate_Should_Not_Continue_When_CompositeFunction_Says_To_Stop_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(),
                                               It.IsAny<bool>(),
                                               It.IsAny<object?>(),
                                               It.IsAny<object?>(),
                                               It.IsAny<IExpressionEvaluator>(),
                                               It.IsAny<IExpression>()))
            .Returns<ICompositeFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
            ((function, isFirstItem, value, context, evaluator, expression)
            => CompositeFunctionEvaluatorResultBuilder.Supported
                .WithResult(tempResult)
                .WithShouldContinue(isFirstItem)
                .Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Evaluate_Should_Return_ErrorMessage_When_CompositeFunction_Gives_Error_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Error("Kaboom").Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().BeEquivalentTo("Kaboom");
    }

    [Fact]
    public void Evaluate_Should_Return_ErrorMessage_When_CompositeFunction_Gives_Error_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(),
                                       It.IsAny<bool>(),
                                       It.IsAny<object?>(),
                                       It.IsAny<object?>(),
                                       It.IsAny<IExpressionEvaluator>(),
                                       It.IsAny<IExpression>()))
    .Returns<ICompositeFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
    ((function, isFirstItem, value, context, evaluator, expression) =>
        isFirstItem ? CompositeFunctionEvaluatorResultBuilder.Supported.Build()
                    : CompositeFunctionEvaluatorResultBuilder.Error("Kaboom").Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().BeEquivalentTo("Kaboom");
    }

    [Fact]
    public void Can_Use_Conditions_On_TryEvaluate()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((context , _)=> Convert.ToInt32(context) == 1);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).Build());
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>(new[] { new ConditionBuilder().WithLeftExpression(new FieldExpressionBuilder("SourceValue")).WithOperator(Operator.Equal).WithRightExpression(new ConstantExpressionBuilder(1)).Build() } ));
        expressionMock.SetupGet(x => x.CompositeFunction)
                      .Returns(new Mock<ICompositeFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Evaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }
}
