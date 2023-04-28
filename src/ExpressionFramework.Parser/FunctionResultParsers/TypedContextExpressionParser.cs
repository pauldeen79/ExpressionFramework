using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.FunctionResultParsers
{
    public class TypedContextExpressionParser : ExpressionParserBase
    {
        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Expression> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator)
        {
            throw new System.NotImplementedException();
        }

        public TypedContextExpressionParser(CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser) : base(parser, @"TypedContext")
        {
        }
    }
}

