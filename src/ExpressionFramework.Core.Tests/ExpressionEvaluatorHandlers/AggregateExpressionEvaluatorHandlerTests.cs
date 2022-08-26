namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class AggregateExpressionEvaluatorHandlerTests
{
    [Fact]
    public void Handle_Returns_NotSupported_When_Expression_Is_Not_An_AggregateExpression()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.NotSupported);
    }

    [Fact]
    public void Handle_Returns_Success_When_Expression_Is_An_AggregateExpression_And_AggregateFunction_Is_Known()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns(true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        object tempResult = 1 + 2;
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(tempResult).Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression) => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Handle_Returns_Invalid_When_Expression_Is_An_AggregateExpression_And_AggregateFunction_Is_Unknown_On_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns(true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(), 
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.NotSupported());
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().StartWith("Unknown aggregate function: [Mock<IAggregateFunction").And.EndWith(">.Object]");
    }

    [Fact]
    public void Handle_Returns_Invalid_When_Expression_Is_An_AggregateExpression_And_AggregateFunction_Is_Unknown_On_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>()))
                              .Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
            .Returns<IAggregateFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
            ((function, isFirstItem, value, context, evaluator, expression)
            => isFirstItem
                ? Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(tempResult).WithContinue(isFirstItem).Build())
                : Result<IAggregateFunctionResultValue?>.NotSupported());
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().StartWith("Unknown aggregate function: [Mock<IAggregateFunction").And.EndWith(">.Object]");
    }

    [Fact]
    public void Handle_Returns_Invalid_When_Expression_Is_An_AggregateExpression_And_AggregateFunction_Is_Known_And_No_Expressions_Are_Provided()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder().Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>());
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Invalid);
        actual.ErrorMessage.Should().Be("No expressions found");
    }

    [Fact]
    public void Handle_Should_Not_Continue_When_AggregateFunction_Says_To_Stop_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(tempResult).Stop().Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Handle_Should_Not_Continue_When_AggregateFunction_Says_To_Stop_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
            .Returns<IAggregateFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
            ((function, isFirstItem, value, context, evaluator, expression)
            => Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(tempResult).WithContinue(isFirstItem).Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void Handle_Should_Return_ErrorMessage_When_AggregateFunction_Gives_Error_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Error("Kaboom"));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().BeEquivalentTo("Kaboom");
    }

    [Fact]
    public void Handle_Should_Return_ErrorMessage_When_ExpressionEvaluator_Gives_Error_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder().Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Error("Kaboom"));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeFalse();
        actual.Status.Should().Be(ResultStatus.Error);
        actual.ErrorMessage.Should().BeEquivalentTo("Kaboom");
    }

    [Fact]
    public void Handle_Should_Return_ErrorMessage_When_AggregateFunction_Gives_Error_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
    .Returns<IAggregateFunction, bool, object?, object?, IExpressionEvaluator, IExpression>
    ((function, isFirstItem, value, context, evaluator, expression) =>
        isFirstItem ? Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder().Build())
                    : Result<IAggregateFunctionResultValue?>.Error("Kaboom"));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>());
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

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
        var evaluatorMock = new Mock<IAggregateFunctionEvaluator>();
        object tempResult = 1;
        evaluatorMock.Setup(x => x.Evaluate(It.IsAny<IAggregateFunction>(),
                                            It.IsAny<bool>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<object?>(),
                                            It.IsAny<IExpressionEvaluator>(),
                                            It.IsAny<IExpression>()))
                     .Returns(Result<IAggregateFunctionResultValue?>.Success(new AggregateFunctionResultValueBuilder(tempResult).Build()));
        var sut = new AggregateExpressionEvaluatorHandler(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IAggregateExpression>();
        expressionMock.SetupGet(x => x.Expressions)
                      .Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions)
                      .Returns(new ReadOnlyValueCollection<ICondition>(new[] { new ConditionBuilder().WithLeftExpression(new FieldExpressionBuilder("SourceValue")).WithOperator(Operator.Equal).WithRightExpression(new ConstantExpressionBuilder(1)).Build() } ));
        expressionMock.SetupGet(x => x.AggregateFunction)
                      .Returns(new Mock<IAggregateFunction>().Object);
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>()))
                               .Returns<object?, object?, IExpression>((item, context, expression)
                               => Result<object?>.Success(((IConstantExpression)expression).Value));

        // Act
        var actual = sut.Handle(default, default, expressionMock.Object, expressionEvaluatorMock.Object);

        // Assert
        actual.IsSuccessful().Should().BeTrue();
        actual.Value.Should().BeEquivalentTo(tempResult);
    }
}
