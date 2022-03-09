﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 6.0.3
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionFramework.Core.DomainModel
{
#nullable enable
    public partial record FieldExpression : ExpressionFramework.Abstractions.DomainModel.IFieldExpression
    {
        public string FieldName
        {
            get;
        }

        public ExpressionFramework.Abstractions.DomainModel.IExpressionFunction? Function
        {
            get;
        }

        public ExpressionFramework.Abstractions.DomainModel.Builders.IExpressionBuilder ToBuilder()
        {
            return new ExpressionFramework.Core.DomainModel.Builders.FieldExpressionBuilder(this);
        }

        public FieldExpression(string fieldName, ExpressionFramework.Abstractions.DomainModel.IExpressionFunction? function)
        {
            this.FieldName = fieldName;
            this.Function = function;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }
    }
#nullable restore
}

