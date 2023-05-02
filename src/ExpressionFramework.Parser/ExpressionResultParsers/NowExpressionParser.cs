using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.ExpressionResultParsers
{
    public class NowExpressionParser : ExpressionParserBase
    {
        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        {
            throw new NotImplementedException();
        }

        public NowExpressionParser() : base(@"Now")
        {
        }
    }
}

