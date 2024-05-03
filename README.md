# ExpressionFramework

Evaluates dynamic expressions at run-time without reflection or dynamic code compilation.
This allows you to validate data at a specific level. (code injection safe)

Example:

```C#
var expression = new FieldExpression("Name");
var context = new { Name = "Hello world!" };
var result = expression.Evaluate(context).Value;
// generates: Hello world!
```

We also support evaluating expression string, like this:

```C#
var parser = _scope.ServiceProvider.GetRequiredService<IExpressionStringParser>();
var result = parser.Parse("=LEFT(\"Hello world!\", 5)", CultureInfo.InvariantCulture);
// generates: Hello
```

See unit tests for more examples.

# Code generation

I am currently not storing generated files in the code repository.
To generate, simply run the console project from either Visual Studio (hit F5) or a command prompt.
This will replace almost all generated code.

There are some files (evaluatables, expressions, operators and aggregators) being generated for the first time only.
This is known as code scaffolding, which happens when you add a new evaluatable, expression, operator or aggregator type.