﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 9.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
#nullable enable
namespace ExpressionFramework.Parser.EvaluatableResultParsers
{
    public class ComposableEvaluatableParser : EvaluatableParserBase
    {
        public ComposableEvaluatableParser() : base(@"ComposableEvaluatable")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            var operatorResult = functionParseResult.GetArgumentExpressionResult<ExpressionFramework.Domain.Operator>(1, @"Operator", functionParseResult.Context, evaluator, parser);
            var combinationResult = functionParseResult.GetArgumentExpressionResult<ExpressionFramework.Domain.Domains.Combination>(3, @"Combination", functionParseResult.Context, evaluator, parser, default);
            var startGroupResult = functionParseResult.GetArgumentExpressionResult<System.Boolean>(4, @"StartGroup", functionParseResult.Context, evaluator, parser);
            var endGroupResult = functionParseResult.GetArgumentExpressionResult<System.Boolean>(5, @"EndGroup", functionParseResult.Context, evaluator, parser);
            var error = new Result[]
            {
                operatorResult,
                combinationResult,
                startGroupResult,
                endGroupResult,
            }.FirstOrDefault(x => !x.IsSuccessful());
            if (error is not null)
            {
                return Result.FromExistingResult<ExpressionFramework.Domain.Evaluatable>(error);
            }
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Evaluatable>(new ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable(
                new TypedConstantResultExpression<System.Object>(functionParseResult.GetArgumentValueResult(0, @"LeftExpression", functionParseResult.Context, evaluator, parser)),
                operatorResult.Value!,
                new TypedConstantResultExpression<System.Object>(functionParseResult.GetArgumentValueResult(2, @"RightExpression", functionParseResult.Context, evaluator, parser)),
                combinationResult.Value,
                startGroupResult.Value!,
                endGroupResult.Value!));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class ComposedEvaluatableParser : EvaluatableParserBase
    {
        public ComposedEvaluatableParser() : base(@"ComposedEvaluatable")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            var conditionsResult = functionParseResult.GetArgumentExpressionResult<System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable>>(0, @"Conditions", functionParseResult.Context, evaluator, parser);
            var error = new Result[]
            {
                conditionsResult,
            }.FirstOrDefault(x => !x.IsSuccessful());
            if (error is not null)
            {
                return Result.FromExistingResult<ExpressionFramework.Domain.Evaluatable>(error);
            }
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Evaluatable>(new ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable(
                conditionsResult.Value!));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class ConstantEvaluatableParser : EvaluatableParserBase
    {
        public ConstantEvaluatableParser() : base(@"ConstantEvaluatable")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            var valueResult = functionParseResult.GetArgumentExpressionResult<System.Boolean>(0, @"Value", functionParseResult.Context, evaluator, parser);
            var error = new Result[]
            {
                valueResult,
            }.FirstOrDefault(x => !x.IsSuccessful());
            if (error is not null)
            {
                return Result.FromExistingResult<ExpressionFramework.Domain.Evaluatable>(error);
            }
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Evaluatable>(new ExpressionFramework.Domain.Evaluatables.ConstantEvaluatable(
                valueResult.Value!));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class DelegateEvaluatableParser : EvaluatableParserBase
    {
        public DelegateEvaluatableParser() : base(@"DelegateEvaluatable")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            var valueResult = functionParseResult.GetArgumentExpressionResult<System.Func<System.Boolean>>(0, @"Value", functionParseResult.Context, evaluator, parser);
            var error = new Result[]
            {
                valueResult,
            }.FirstOrDefault(x => !x.IsSuccessful());
            if (error is not null)
            {
                return Result.FromExistingResult<ExpressionFramework.Domain.Evaluatable>(error);
            }
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Evaluatable>(new ExpressionFramework.Domain.Evaluatables.DelegateEvaluatable(
                valueResult.Value!));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class SingleEvaluatableParser : EvaluatableParserBase
    {
        public SingleEvaluatableParser() : base(@"SingleEvaluatable")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Evaluatable> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            var operatorResult = functionParseResult.GetArgumentExpressionResult<ExpressionFramework.Domain.Operator>(1, @"Operator", functionParseResult.Context, evaluator, parser);
            var error = new Result[]
            {
                operatorResult,
            }.FirstOrDefault(x => !x.IsSuccessful());
            if (error is not null)
            {
                return Result.FromExistingResult<ExpressionFramework.Domain.Evaluatable>(error);
            }
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Evaluatable>(new ExpressionFramework.Domain.Evaluatables.SingleEvaluatable(
                new TypedConstantResultExpression<System.Object>(functionParseResult.GetArgumentValueResult(0, @"LeftExpression", functionParseResult.Context, evaluator, parser)),
                operatorResult.Value!,
                new TypedConstantResultExpression<System.Object>(functionParseResult.GetArgumentValueResult(2, @"RightExpression", functionParseResult.Context, evaluator, parser))));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
}
#nullable disable
