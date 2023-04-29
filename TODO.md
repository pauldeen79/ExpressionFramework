# //TODO
expressions:
- Add RangeExpression, which creates a range from minimum to maximum value (optionally with step)
- Add StringJoinExpression, which joins multiple values using a separator
- Add PadLeft and PadRight string expressions
- Add StringFormat and StringJoin string expressions
- Add CreateObject expression, that creates an object (expando object) with multiple properties
- Add Coalesce/FirstNotNull expression, which maybe is just a short-hand for First with a predicate, but maybe is worth to have on its own
- Add expressions: IsOfType, IsNotOfType, IsNotNull, IsNull, IsNotEmptyString, IsEmptyString
- Add expressions: ConvertToInt, ConvertToDouble, ConvertToDecimal, ConvertToBoolean, ParseDateTime, ConvertToString
- Add expressions: DateAdd(expression, part, number), CreateDateTime(year, month, day, hour, minute, second), Hour, Minute, Second
- Add TypedSequenceExpression<T>
- Add FormattableStringExpression, with a formattable string (you might also used the generic ConstantExpression, but it seems logical to have a special cased one for this in the front-end so you can recognize it)
- Add TypedSwitchExpression<T>, TypedCase<T> and TypedEvaluatable<T> as a kind of switch expression
- Add overloads for common operators like ==, !=, <, <=, >, >= so they can be used in string expressions i.e. =Function1() == Function2().
  Split by the sign/operator using escape double quote and leave the double quote. When split count is 2, then it's valid else just continue.

general:
- Review if we want to add validation for parameters?
- Generate EvaluatableResultParsers (scaffold).
  They can al be simply "newed", so you just have to register them as functions