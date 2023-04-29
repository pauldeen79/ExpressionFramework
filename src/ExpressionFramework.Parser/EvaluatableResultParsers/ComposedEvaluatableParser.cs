using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.EvaluatableResultParsers
{
    public class ComposedEvaluatableParser : EvaluatableParserBase
    {
        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator)
        {
            throw new System.NotImplementedException();
        }

        public ComposedEvaluatableParser(CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser) : base(parser, @"ComposedEvaluatable")
        {
        }
    }
}

