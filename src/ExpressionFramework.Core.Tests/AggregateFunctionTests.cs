﻿namespace ExpressionFramework.Core.Tests;

public class AggregateFunctionTests : TestBase<AggregateFunction>
{
    public class Evaluate : AggregateFunctionTests
    {
        [Fact]
        public void Returns_Invalid_On_Missing_Arguments()
        {
            // Arrange
            var functionCall = new FunctionCallBuilder().WithName("Aggregate");
            var functionEvaluator = Fixture.Freeze<IFunctionEvaluator>();
            var expressionEvaluator = Fixture.Freeze<IExpressionEvaluator>();
            var context = new FunctionCallContext(functionCall, functionEvaluator, expressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
            result.ErrorMessage.ShouldBe("Missing argument: Expressions");
        }

        [Fact]
        public void Returns_Invalid_On_Empty_Sequence_Expressions()
        {
            // Arrange
            var sequence = Enumerable.Empty<object>();
            var aggregator = Fixture.Freeze<IAggregator>();
            var functionCall = new FunctionCallBuilder()
                .WithName("Aggregate")
                .AddArguments(
                    new ConstantArgumentBuilder().WithValue(sequence),
                    new ConstantArgumentBuilder().WithValue(aggregator)
                );

            var functionEvaluator = Fixture.Freeze<IFunctionEvaluator>();
            var expressionEvaluator = Fixture.Freeze<IExpressionEvaluator>();
            var context = new FunctionCallContext(functionCall, functionEvaluator, expressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Invalid);
            result.ErrorMessage.ShouldBe("Sequence contains no elements");
        }

        [Fact]
        public void Returns_Success_On_Valid_Arguments()
        {
            // Arrange
            var sequence = new object[] { 1, 2, 3 };
            var aggregator = new AddAggregator();
            var functionCall = new FunctionCallBuilder()
                .WithName("Aggregate")
                .AddArguments(
                    new ConstantArgumentBuilder().WithValue(sequence),
                    new ConstantArgumentBuilder().WithValue(aggregator)
                );

            var functionEvaluator = Fixture.Freeze<IFunctionEvaluator>();
            var expressionEvaluator = Fixture.Freeze<IExpressionEvaluator>();
            var context = new FunctionCallContext(functionCall, functionEvaluator, expressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Ok);
            result.Value.ShouldBeEquivalentTo(1 + 2 + 3);
        }

        [Fact]
        public void Returns_NonSuccessful_Result_From_Aggregator_When_This_Occurs()
        {
            // Arrange
            var sequence = new object[] { 1, 2, 3 };
            var aggregator = Fixture.Freeze<IAggregator>();
            aggregator
                .Aggregate(Arg.Any<object>(), Arg.Any<object>(), Arg.Any<IFormatProvider>())
                .Returns(Result.Error<object?>("Kaboom"));

            var functionCall = new FunctionCallBuilder()
                .WithName("Aggregate")
                .AddArguments(
                    new ConstantArgumentBuilder().WithValue(sequence),
                    new ConstantArgumentBuilder().WithValue(aggregator)
                );

            var functionEvaluator = Fixture.Freeze<IFunctionEvaluator>();
            var expressionEvaluator = Fixture.Freeze<IExpressionEvaluator>();
            var context = new FunctionCallContext(functionCall, functionEvaluator, expressionEvaluator, new FunctionEvaluatorSettingsBuilder(), null);
            var sut = CreateSut();

            // Act
            var result = sut.Evaluate(context);

            // Assert
            result.Status.ShouldBe(ResultStatus.Error);
            result.ErrorMessage.ShouldBe("Kaboom");
        }
    }
}
