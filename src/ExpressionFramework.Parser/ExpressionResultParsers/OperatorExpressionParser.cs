using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.ExpressionResultParsers
{
    public class OperatorExpressionParser : ExpressionParserBase
    {
        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        {
            throw new NotImplementedException();
        }

        public OperatorExpressionParser() : base(@"Operator")
        {
        }
    }
}

