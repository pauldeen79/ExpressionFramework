using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Parser.FunctionResultParsers
{
    public class NotEqualsExpressionParser : CrossCutting.Utilities.Parsers.Contracts.IFunctionResultParser, ExpressionFramework.Parser.Contracts.IExpressionParser
    {
        public CrossCutting.Common.Results.Result<object?> Parse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, object? context, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator)
        {
            var result = Parse(functionParseResult, evaluator);
            if (!result.IsSuccessful() || result.Status == CrossCutting.Common.Results.ResultStatus.Continue)
            {
                return Result<object?>.FromExistingResult(result);
            }
            return result.Value!.Evaluate(context);
        }

        public CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Expression> Parse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator)
        {
            if (functionParseResult.FunctionName.ToUpperInvariant() != "NOTEQUALS")
            {
                return CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Expression>.Continue();
            }
            throw new System.NotImplementedException();
        }

        public NotEqualsExpressionParser(CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            _parser = parser;
        }

        private readonly CrossCutting.Utilities.Parsers.Contracts.IExpressionParser _parser;
    }
}

