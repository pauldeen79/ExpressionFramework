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

# Code generation

Due to some limitations of the used code generator (of which I am the author by the way), you have to generate the code generation manually.
To do this, you go to the one and only unit test in the ExpressionFramework.Abstractions.Tests project, change the `dryrun` setting to false, run the unit test, and revert this change.
This way, the code generation model is updated. You have to check in this file to git.
After building the solution, the code generation will run in the post build event of the CodeGeneration project.
This works both locally and in build pipelines. (e.g. Github Actions)