# ExpressionFramework
Evaluates expressions and conditions.

Example:
```C#
//using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection.AddExpressionFramework().BuildServiceProvider();

// Expression evaluation:
var expressionEvaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();
var expression = new FieldExpressionBuilder("Name").Build();
var context = new { Name = "Hello world!" };
var result = expressionEvaluator.Evaluate(context, expression);
// generates: Hello world!

// Condition evaluation:
var conditionEvaluator = serviceProvider.GetRequiredService<IConditionEvaluator>();
var condition = new ConditionBuilder()
    .WithLeftExpression(new ConstantExpressionBuilder("12345"))
    .WithOperator(Operator.Equal)
    .WithRightExpression(new ConstantExpressionBuilder("12345"))
    .Build();
var result = conditionEvaluator.Evaluate(null, new[] { condition });
// generates: true
```

See unit tests for more examples.

TODO:
- Add functions: IsOfType, IsNotOfType, IsNotEmpty, IsEmpty, ConvertToInt, ConvertToDouble, ConvertToBoolean, ParseDateTime, ToString, Coalesce
- For IsNotEmpty and IsEmpty, write it so it can be extended by type using IoC with components which has a boolean CanDetermine function, and a Determine function. Default implementation is last.
- Add composite functions: Min, Max, First, Last, ElementAt
- Check if we can refactor functions to inner expressions. e.g. new FieldExpressionBuilder("FieldName").WithInnerExpression(new ToUpperExpressionBuilder());
- Think if we want to be able to preprocess expressions, like sorting... the filtering is now hard-coded into the CompositeExpressionEvaluatorProvider, which might be wrong. Can't alter it from the expression right now.
