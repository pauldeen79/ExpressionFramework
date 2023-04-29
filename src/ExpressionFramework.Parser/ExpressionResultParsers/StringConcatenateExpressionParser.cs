using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.ExpressionResultParsers
{
    public class StringConcatenateExpressionParser : ExpressionParserBase
    {
        protected override Result<Expression> DoParse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        {
            throw new NotImplementedException();
        }

        public StringConcatenateExpressionParser(IExpressionParser parser) : base(parser, @"StringConcatenate")
        {
        }
    }
}

