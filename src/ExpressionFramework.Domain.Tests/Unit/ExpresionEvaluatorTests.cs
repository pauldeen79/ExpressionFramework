﻿using CrossCutting.Common.Extensions;

namespace ExpressionFramework.Domain.Tests.Unit;

public sealed class ExpresionEvaluatorTests : IDisposable
{
    private IExpressionEvaluator CreateSut() => _provider.GetRequiredService<IExpressionEvaluator>();
    private readonly ServiceProvider _provider;

    public ExpresionEvaluatorTests()
    {
        _provider = new ServiceCollection().AddExpressionFramework().BuildServiceProvider();
    }

    [Fact]
    public async Task Evaluate_Happy_Flow()
    {
        // Arrange
        var expressionEvaluator = CreateSut();
        var expression = new FieldExpressionBuilder().WithFieldName("Name").Build();
        var context = new { Name = "Hello world!" };

        // Act
        var result = await expressionEvaluator.Evaluate(context, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world!");
    }

    [Fact]
    public async Task Can_Evaluate_Nested_FieldExpression_Using_DuckTyping()
    {
        // Arrange
        var expression = new FieldExpressionBuilder().WithFieldName("InnerProperty.Name").Build();
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var result = await CreateSut().Evaluate(context, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world");
    }

    [Fact]
    public async Task Can_Evaluate_Nested_FieldExpression_Using_ChainedExpression()
    {
        // Arrange
        var expression = new ChainedExpressionBuilder()
            .AddExpressions
            (
                new FieldExpressionBuilder().WithFieldName("InnerProperty"),
                new FieldExpressionBuilder().WithFieldName("Name")
            )
            .Build();
        var context = new { InnerProperty = new { Name = "Hello world" } };

        // Act
        var result = await CreateSut().Evaluate(context, expression);

        // Assert
        result.Status.Should().Be(ResultStatus.Ok);
        result.Value.Should().Be("Hello world");
    }

    public void Dispose()
        => _provider.Dispose();
}
