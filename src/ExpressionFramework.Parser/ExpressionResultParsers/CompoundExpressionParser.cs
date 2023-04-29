using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.ExpressionResultParsers
{
    public class CompoundExpressionParser : ExpressionParserBase
    {
        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        {
            throw new NotImplementedException();
        }

        public CompoundExpressionParser(IExpressionParser parser) : base(parser, @"Compound")
        {
        }
    }
}

