using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.ExpressionResultParsers
{
    public class NowExpressionParser : ExpressionParserBase
    {
        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        {
            throw new NotImplementedException();
        }

        public NowExpressionParser(IExpressionParser parser) : base(parser, @"Now")
        {
        }
    }
}

