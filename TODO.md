# //TODO
expression evaluator:
- Add aggregate functions: Min, Max, First, Last, ElementAt, Count, Sum
- Think if we want to be able to preprocess expressions in the aggregate expression evaluator, like sorting... the filtering is now hard-coded into the AggregateExpressionEvaluatorProvider, which might be wrong. Can't alter it from the expression right now.
- Add SwitchExpression, which makes one of the scenarios in the integration tests a little easier.
- Add SequenceExpression, which evaluates all expressions, and returns them as a sequence.
- Add WhereExpression, which filters expressions by conditions.
- Refactor functions to expressions. You can use them in a CompositeExpression for pre-processing or post-processing values.
- Add functions/expressions: IsOfType, IsNotOfType, IsNotEmpty, IsEmpty, ConvertToInt, ConvertToDouble, ConvertToBoolean, ParseDateTime, ToString, Coalesce
- For IsNotEmpty and IsEmpty, write it so it can be extended by type using IoC with components which has a boolean CanDetermine function, and a Determine function. Default implementation is last.

condition evaluation:
-replace object? return value with Result<object?>
-check if we have extension method IsEqualToAny, StartsWithAny, EndsWithAny, IsNotEqualToAny, NotStartsWithAny and EndsWithAny
 e.g. .Where("Field").IsEqualToAny("A", "B")
-check if we can support Equals, StartsWith, EndsWith, NotEquals for multiple values, when the value is an IEnumerable (and not of type string)
 e.g. .Where("Field").IsEqualTo(new[] { "A", "B" })
-add faster evaluation when the conditions don't contain OR or brackets. You can simply evaluate every condition, return false on the first failing one, or else return true

code generation:
-check if we can upgrade to latest version of model framework
-check if we can refactor to non-inheritance, add null checks in c'tors, and remove static builder extensions (which is a pain in the ***)
-add template file for generation of code generation models, instead of unit test with dryrun = false/true
