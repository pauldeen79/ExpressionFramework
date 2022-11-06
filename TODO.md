# //TODO
expressions:
- Add RangeExpression, which creates a range from minimum to maximum value (optionally with step)
- Add StringJoinExpression, which joins multiple values using a separator
- Add PadLeft and PadRight string expressions
- Add CreateObject expression, that creates an object (expando object) with multiple properties
- Add Coalesce/FirstNotNull expression, which maybe is just a short-hand for First with a predicate, but maybe is worth to have on its own
- Add expressions: IsOfType, IsNotOfType, IsNotNull, IsNull, IsNotEmptyString, IsEmptyString
- Add expressions: ConvertToInt, ConvertToDouble, ConvertToDecimal, ConvertToBoolean, ParseDateTime, ConvertToString
- Add expressions: DateAdd(expression, part, numbe), CreateDateTime(year, month, day, hour, minute, second)
- Add TypedSequenceExpression<T>

general:
- Review if we want to add validation for parameters?

expression parser, which parses a string into an expression (so you can evaluate it)
I think syntax will be like Excel and SQL

Description       Excel                  Sql
----------------- ---------------------- ------------------------------------
Literal value     Some value             'Some value'
                  1                      1
                  1.45                   1.45
                  10/1/2022              2022-10-01
                  TRUE                   1
Computation       =1+1                   1+1
Conditional       =IF(A1="Nee";1;0)      IIF(A1='Nee',1,0)
Switch            =SWITCH(A1;"Nee";1;0)  CASE A1 WHEN 'Nee' THEN 1 ELSE 0 END

Decision: Try to stick with Excel syntax, that's more intuitive.

Rules:
* If it doesn't start with an equals sign, it's an literal value (constant expression)
  * If it's true or false, then it's a boolean
  * If it's ISO date format, then it's a datetime
  * Try decimal.TryParse, int.TryParse. when success, it's a numeric value of this type (decimal/int)
  * In other cases it's a string literal
* If if starts with an equal sign, then it's an dynamic value (non-constant expression) that needs to be parsed
  * Find first close parenthesis ) and corresponding open paranthesis (
    * When not found, try operators like + - / * ^ % & using split --> note that this means we don't support groups in computations yet, like (1+3)/2
      * When count > 0, then compute result using split values of the operators, taking operator order into account (MVDWOA)
      * When count = 0, then the entire value (next to the = sign) is tried as a function name
        Find the function name by checking all known expressions, removing the 'Expression' prefix, or using custom value using attribute
      * When function name is empty, then it's an error/notsupported expression with message: Computation with parenthesis are not supported at this time
      * When function name is not recognized, then it's an error expression with message: Unknown expression: <name>
      * When function name is recognized, then pass arguments to context and/or parameters
        Find the context and parameters using the descriptor (note that parameters are just taken in the same order. if this is problematic, then we might introduce a new property on the parameter to explicity set order)
          * When context is used, then the first parameter will be mapped to the context, subsequent parameters to parameters
          * When context is not used, the first parameter will be mapped to the first parameter, subsequent parameters to parameters
        Use conversion, like for example conditions, strings etc. For conditions, you need operator conversion like = <> < > <= >=


Some examples:
Some value
  new TypedLiteralExpression<string>("Some value")
1
  new TypedLiteralExpression<int>(1)
1.45
  new TypedLiteralExpression<decimal>(1.45M)
10/1/2022
  new TypedLiteralExpression<DateTime>(2022, 10, 1)
True
TRUE
  new TypedLiteralExpression<bool>(true)
=1+1
  new CompoundExpression(2, new AddAgregator()).Evaluate(1)
  or
  new AggregateExpression(new object[] { 2 }, new AddAggregator()).Evaluate(1)
=1+2+3
  new AggregateExpression(new object[] { 2, 3 }, new AddAggregator()).Evaluate(1)
=(1+3)/2
  new ErrorExpression("Computation with paranthesis are not supported at this time")
=IF("A1"="Nee";1;0)
  new IfExpression(new SingleEvaluatable(new TypedLiteralExpression<string>("A1"), new EqualsOperator(), new TypedLiteralExpression("Nee")), new TypedLiteralExpression<int>(1), new TypedLiteralExpression<int>(0))
