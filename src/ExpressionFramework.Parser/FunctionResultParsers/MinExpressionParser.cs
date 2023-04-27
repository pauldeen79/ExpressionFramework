using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.FunctionResultParsers
{
    public class MinExpressionParser : CrossCutting.Utilities.Parsers.Contracts.IFunctionResultParser
    {
        public Result<object?> Parse(FunctionParseResult functionParseResult, IFunctionParseResultEvaluator evaluator)
        {
            if (functionParseResult.FunctionName.ToUpperInvariant() != "MIN")
            {
                return CrossCutting.Common.Results.Result<object?>.Continue();
            }
            throw new System.NotImplementedException();
        }
    }
}

