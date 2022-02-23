# ExpressionFramework
Evaluates expressions and conditions.

Example:
```C#
//using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection.AddExpressionFramework().BuildServiceProvider();
var evaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();
var expression = new FieldExpressionBuilder().WithFieldName("Name").Build();
var context = new { Name = "Hello world!" };
var result = evaluator.Evaluate(context, expression);
// generates: Hello world!
```

See unit tests for more examples.
