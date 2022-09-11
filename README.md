# ExpressionFramework
Evaluates expressions and conditions.

Example:
```C#
//using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection.AddExpressionFramework().BuildServiceProvider();

// Expression evaluation:
var expressionEvaluator = serviceProvider.GetRequiredService<IExpressionEvaluator>();
var expression = new FieldExpression("Name");
var context = new { Name = "Hello world!" };
var result = await expressionEvaluator.Evaluate(context, expression).Value;
// generates: Hello world!

// Condition evaluation:
var conditionEvaluator = serviceProvider.GetRequiredService<IConditionEvaluator>();
var condition = new Condition
(
    new ConstantExpression("12345"),
    Operator.Equal,
    new ConstantExpression("12345")
);
var result = await conditionEvaluator.Evaluate(null, new[] { condition });
// generates: true
```

See unit tests for more examples.

# Code generation

I am currently not storing generated files in the code repository.
To generate, simply run the console project from either Visual Studio (hit F5) or a command prompt.
This will replace almost all generated code.

There are some files (operator handlers and expression handlers) being generated for the first time only.
This is known as code scaffolding, which happens when you add a new expression or operator type.

Command to run code generation:
```bash
dotnet run --project ./src/ExpressionFramework.CodeGeneration/
```
