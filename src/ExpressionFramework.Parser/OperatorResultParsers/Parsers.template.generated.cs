﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.6
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
namespace ExpressionFramework.Parser.OperatorResultParsers
{
    public class EndsWithOperatorParser : OperatorParserBase
    {
        public EndsWithOperatorParser() : base(@"EndsWithOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.EndsWithOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class EnumerableContainsOperatorParser : OperatorParserBase
    {
        public EnumerableContainsOperatorParser() : base(@"EnumerableContainsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.EnumerableContainsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class EnumerableNotContainsOperatorParser : OperatorParserBase
    {
        public EnumerableNotContainsOperatorParser() : base(@"EnumerableNotContainsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.EnumerableNotContainsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class EqualsOperatorParser : OperatorParserBase
    {
        public EqualsOperatorParser() : base(@"EqualsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.EqualsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsGreaterOperatorParser : OperatorParserBase
    {
        public IsGreaterOperatorParser() : base(@"IsGreaterOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsGreaterOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsGreaterOrEqualOperatorParser : OperatorParserBase
    {
        public IsGreaterOrEqualOperatorParser() : base(@"IsGreaterOrEqualOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsGreaterOrEqualOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNotNullOperatorParser : OperatorParserBase
    {
        public IsNotNullOperatorParser() : base(@"IsNotNullOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNotNullOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNotNullOrEmptyOperatorParser : OperatorParserBase
    {
        public IsNotNullOrEmptyOperatorParser() : base(@"IsNotNullOrEmptyOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNotNullOrEmptyOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNotNullOrWhiteSpaceOperatorParser : OperatorParserBase
    {
        public IsNotNullOrWhiteSpaceOperatorParser() : base(@"IsNotNullOrWhiteSpaceOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNotNullOrWhiteSpaceOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNullOperatorParser : OperatorParserBase
    {
        public IsNullOperatorParser() : base(@"IsNullOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNullOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNullOrEmptyOperatorParser : OperatorParserBase
    {
        public IsNullOrEmptyOperatorParser() : base(@"IsNullOrEmptyOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNullOrEmptyOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsNullOrWhiteSpaceOperatorParser : OperatorParserBase
    {
        public IsNullOrWhiteSpaceOperatorParser() : base(@"IsNullOrWhiteSpaceOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsNullOrWhiteSpaceOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsSmallerOperatorParser : OperatorParserBase
    {
        public IsSmallerOperatorParser() : base(@"IsSmallerOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsSmallerOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class IsSmallerOrEqualOperatorParser : OperatorParserBase
    {
        public IsSmallerOrEqualOperatorParser() : base(@"IsSmallerOrEqualOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.IsSmallerOrEqualOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class NotEndsWithOperatorParser : OperatorParserBase
    {
        public NotEndsWithOperatorParser() : base(@"NotEndsWithOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.NotEndsWithOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class NotEqualsOperatorParser : OperatorParserBase
    {
        public NotEqualsOperatorParser() : base(@"NotEqualsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.NotEqualsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class NotStartsWithOperatorParser : OperatorParserBase
    {
        public NotStartsWithOperatorParser() : base(@"NotStartsWithOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.NotStartsWithOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class StartsWithOperatorParser : OperatorParserBase
    {
        public StartsWithOperatorParser() : base(@"StartsWithOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.StartsWithOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class StringContainsOperatorParser : OperatorParserBase
    {
        public StringContainsOperatorParser() : base(@"StringContainsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.StringContainsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
    public class StringNotContainsOperatorParser : OperatorParserBase
    {
        public StringNotContainsOperatorParser() : base(@"StringNotContainsOperator")
        {
        }

        protected override CrossCutting.Common.Results.Result<ExpressionFramework.Domain.Operator> DoParse(CrossCutting.Utilities.Parsers.FunctionParseResult functionParseResult, CrossCutting.Utilities.Parsers.Contracts.IFunctionParseResultEvaluator evaluator, CrossCutting.Utilities.Parsers.Contracts.IExpressionParser parser)
        {
            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return Result.Success<ExpressionFramework.Domain.Operator>(new ExpressionFramework.Domain.Operators.StringNotContainsOperator(
    ));
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        }
    }
}
#nullable disable