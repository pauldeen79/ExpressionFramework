using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.EvaluatableResultParsers
{
    public class SingleEvaluatableParser : EvaluatableParserBase
    {
        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, IExpressionParser parser)
        {
            throw new System.NotImplementedException();
        }

        public SingleEvaluatableParser() : base(@"SingleEvaluatable")
        {
        }
    }
}

