﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Domain.Expressions
{
#nullable enable
    public partial record CastExpression<T>
    {
        public override CrossCutting.Common.Results.Result<object?> Evaluate(object? context)
        {
            throw new System.NotImplementedException();
        }
    }
#nullable restore
}
