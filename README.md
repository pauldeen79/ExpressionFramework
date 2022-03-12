# ExpressionFramework
Evaluates expressions and conditions.

Example:
```C#
//using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection.AddExpressionFramework().BuildServiceProvider();

// Expression evaluation:
var expressionEvaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();
var expression = new FieldExpressionBuilder().WithFieldName("Name").Build();
var context = new { Name = "Hello world!" };
var result = expressionEvaluator.Evaluate(context, expression);
// generates: Hello world!

// Condition evaluation:
var conditionEvaluator = serviceProvider.GetRequiredService<IConditionEvaluator>();
var condition = new ConditionBuilder()
    .WithLeftExpression(new ConstantExpressionBuilder().WithValue("12345"))
    .WithOperator(Operator.Equal)
    .WithRightExpression(new ConstantExpressionBuilder().WithValue("12345"))
    .Build();
var result = conditionEvaluator.Evaluate(null, new[] { condition });
// generates: true
```

See unit tests for more examples.