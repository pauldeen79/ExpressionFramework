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

general:
- Review if we want to add validation for parameters?
- Expression parser, based on the generic FunctionParser in CrossCutting. format: FUNCTIONNAME(arguments)
  Need to think of a way to replace stuff like 1+1 -> Plus(1, 1) using regular expressions, or use formula parser when available in CrossCutting
- ToString override on Expression, which generates a function string when possible (GenerateFunctionString of type Result<string>), otherwise the name of the expression (should be compatible with parsing of a function string)
- Try to do a POC to restrict input to functions, like Day/Month/Year only accepting Expression<DateTime> -> refactor remaining expressions to implement this
