# //TODO
expression evaluator:
- Add aggregate functions/expressions: Min, Max, First, Last, ElementAt, Count, Sum
- Think if we want to be able to preprocess expressions in the aggregate expression evaluator, like sorting... the filtering is now hard-coded into the AggregateExpressionEvaluatorProvider, which might be wrong. Can't alter it from the expression right now.
- Add SequenceExpression, which evaluates all expressions, and returns them as a sequence.
- Add WhereExpression/FilterExpression, which filters expressions by conditions.
- Add OrderByExpression/SortExpression, which orders expressions.
- Add expressions for each operator on condition, so you can use these as an expression. e.g. EqualsExpression, NotEqualsExpression.
- Refactor functions to expressions. You can use them in a CompositeExpression for pre-processing or post-processing values.
- Refactor aggregate functions to expressions. Use these from the AggregateExpressionEvaluator.
- Add functions/expressions: IsOfType, IsNotOfType, IsNotEmpty, IsEmpty, ConvertToInt, ConvertToDouble, ConvertToBoolean, ParseDateTime, ToString, Coalesce/FirstNotNull

condition evaluation:
-check if we have extension method IsEqualToAny, StartsWithAny, EndsWithAny, IsNotEqualToAny, NotStartsWithAny and EndsWithAny
 e.g. .Where("Field").IsEqualToAny("A", "B")

code generation vNext:
-check if we can refactor to non-inheritance and remove static builder extensions (which is a pain in the ***)
-one-step code generation from a console app
  -models in the code generation project
  -no in-between models or inheritance
  -concrete types, no inheritance or reference to the code generation project from the domain
  -namespace mapping to accomplish this
  -note that enums can be modeled as enums in the code generation project as well (we will use namespace mapping), but don't need values
  -new project which does not have the 'Core' suffix anymore.
  -remove Abstractions project