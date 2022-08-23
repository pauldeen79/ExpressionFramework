using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;
using Newtonsoft.Json.Linq;

namespace ExpressionFramework.Core.Tests.ExpressionEvaluatorProviders;

public class CompositeExpressionEvaluatorProviderTests
{
    [Fact]
    public void TryEvaluate_Returns_False_When_Expression_Is_Not_A_CompositeExpression()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<IExpression>();
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeFalse();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1 + 2;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Unknown()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.NotSupported.Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Returns_True_When_Expression_Is_A_CompositeExpression_And_CompositeFunction_Is_Known_And_No_Expressions_Are_Provided()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>());
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeNull();
    }

    [Fact]
    public void TryEvaluate_Should_Not_Continue_When_CompositeFunction_Says_To_Stop_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).WithShouldContinue(false).Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void TryEvaluate_Should_Not_Continue_When_CompositeFunction_Says_To_Stop_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
        object tempResult = 1;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
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
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo(tempResult);
    }

    [Fact]
    public void TryEvaluate_Should_Return_ErrorMessage_When_CompositeFunction_Gives_Error_In_First_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Error("Kaboom").Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo("Kaboom");
    }

    [Fact]
    public void TryEvaluate_Should_Return_ErrorMessage_When_CompositeFunction_Gives_Error_In_Subsequent_Item()
    {
        // Arrange
        var conditionEvaluatorProviderMock = new Mock<IConditionEvaluatorProvider>();
        var conditionEvaluatorMock = new Mock<IConditionEvaluator>();
        conditionEvaluatorProviderMock.Setup(x => x.Get(It.IsAny<IExpressionEvaluator>())).Returns(conditionEvaluatorMock.Object);
        conditionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<IEnumerable<ICondition>>())).Returns<object?, IEnumerable<ICondition>>((_, _) => true);
        var evaluatorMock = new Mock<ICompositeFunctionEvaluator>();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
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
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>());
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo("Kaboom");
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        evaluatorMock.Setup(x => x.TryEvaluate(It.IsAny<ICompositeFunction>(), It.IsAny<bool>(), It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpressionEvaluator>(), It.IsAny<IExpression>())).Returns(CompositeFunctionEvaluatorResultBuilder.Supported.WithResult(tempResult).Build());
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        var sut = new CompositeExpressionEvaluatorProvider(conditionEvaluatorProviderMock.Object, new[] { evaluatorMock.Object });
        var expressionMock = new Mock<ICompositeExpression>();
        expressionMock.SetupGet(x => x.Expressions).Returns(new ReadOnlyValueCollection<IExpression>(new[] { new ConstantExpressionBuilder(1).Build(), new ConstantExpressionBuilder(2).Build() }));
        expressionMock.SetupGet(x => x.ExpressionConditions).Returns(new ReadOnlyValueCollection<ICondition>(new[] { new ConditionBuilder().WithLeftExpression(new FieldExpressionBuilder("SourceValue")).WithOperator(Operator.Equal).WithRightExpression(new ConstantExpressionBuilder(1)).Build() } ));
        var expressionEvaluatorMock = new Mock<IExpressionEvaluator>();
        expressionEvaluatorMock.Setup(x => x.Evaluate(It.IsAny<object?>(), It.IsAny<object?>(), It.IsAny<IExpression>())).Returns<object?, object?, IExpression>((item, context, expression) => { return ((IConstantExpression)expression).Value; });

        // Act
        var actual = sut.TryEvaluate(default, default, expressionMock.Object, expressionEvaluatorMock.Object, out var result);

        // Assert
        actual.Should().BeTrue();
        result.Should().BeEquivalentTo(tempResult);
    }
}
