# ExpressionFramework
Evaluates expressions.

Example:
```C#
var expression = new FieldExpression("Name");
var context = new { Name = "Hello world!" };
var result = expression.Evaluate(context).Value;
// generates: Hello world!
```

See unit tests for more examples.

# Code generation

I am currently not storing generated files in the code repository.
To generate, simply run the console project from either Visual Studio (hit F5) or a command prompt.
This will replace almost all generated code.

There are some files (operators and expressions) being generated for the first time only.
This is known as code scaffolding, which happens when you add a new expression or operator type.

Command to run code generation:
```bash
dotnet run --project ./src/ExpressionFramework.CodeGeneration/
```
