namespace ExpressionFramework.Core.Tests.TestHelpers;

internal static class TestFixtures
{
    internal static Mock<ICondition> CreateConditionMock()
    {
        var conditionMock = new Mock<ICondition>();
        conditionMock.SetupGet(x => x.LeftExpression)
                     .Returns(new Mock<IExpression>().Object);
        conditionMock.SetupGet(x => x.RightExpression)
                     .Returns(new Mock<IExpression>().Object);
        return conditionMock;
    }

    internal static Mock<IExpressionFunction> CreateFunctionMock()
    {
        var functionMock = new Mock<IExpressionFunction>();
        functionMock.Setup(x => x.ToBuilder())
                    .Returns(new Mock<IExpressionFunctionBuilder>().Object);
        return functionMock;
    }

    internal static Mock<IExpressionFunctionBuilder> CreateFunctionBuilderMock()
    {
        var functionBuilderMock = new Mock<IExpressionFunctionBuilder>();
        functionBuilderMock.Setup(x => x.Build())
                           .Returns(new Mock<IExpressionFunction>().Object);
        return functionBuilderMock;
    }

    internal static Mock<IExpression> CreateExpressionMock()
    {
        var expressionMock = new Mock<IExpression>();
        expressionMock.Setup(x => x.ToBuilder())
                      .Returns(new Mock<IExpressionBuilder>().Object);
        return expressionMock;
    }

    internal static Mock<IExpressionBuilder> CreateExpressionBuilderMock()
    {
        var expressionBuilderMock = new Mock<IExpressionBuilder>();
        expressionBuilderMock.Setup(x => x.Build())
                             .Returns(new Mock<IExpression>().Object);
        return expressionBuilderMock;
    }
}
