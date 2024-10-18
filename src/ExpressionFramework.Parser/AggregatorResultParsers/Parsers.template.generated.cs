﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.10
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace ExpressionFramework.Parser.AggregatorResultParsers
{
    public class AddAggregatorParser : AggregatorParserBase
    {
        public AddAggregatorParser() : base(@"AddAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.AddAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class DivideAggregatorParser : AggregatorParserBase
    {
        public DivideAggregatorParser() : base(@"DivideAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.DivideAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class EnumerableConcatenateAggregatorParser : AggregatorParserBase
    {
        public EnumerableConcatenateAggregatorParser() : base(@"EnumerableConcatenateAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.EnumerableConcatenateAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class ModulusAggregatorParser : AggregatorParserBase
    {
        public ModulusAggregatorParser() : base(@"ModulusAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.ModulusAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class MultiplyAggregatorParser : AggregatorParserBase
    {
        public MultiplyAggregatorParser() : base(@"MultiplyAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.MultiplyAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class PowerAggregatorParser : AggregatorParserBase
    {
        public PowerAggregatorParser() : base(@"PowerAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.PowerAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class StringConcatenateAggregatorParser : AggregatorParserBase
    {
        public StringConcatenateAggregatorParser() : base(@"StringConcatenateAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.StringConcatenateAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class SubtractAggregatorParser : AggregatorParserBase
    {
        public SubtractAggregatorParser() : base(@"SubtractAggregator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Aggregator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Aggregator>(new ExpressionFramework.Domain.Aggregators.SubtractAggregator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
}
#nullable disable
