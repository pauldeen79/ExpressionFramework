﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.10
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
#nullable enable
namespace ExpressionFramework.Domain
{
    public partial record AggregatorDescriptor
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string TypeName
        {
            get;
        }

        public string Description
        {
            get;
        }

        public string ContextTypeName
        {
            get;
        }

        public string ContextDescription
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ParameterDescriptor> Parameters
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ReturnValueDescriptor> ReturnValues
        {
            get;
        }

        public AggregatorDescriptor(string name, string typeName, string description, string contextTypeName, string contextDescription, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ParameterDescriptor> parameters, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ReturnValueDescriptor> returnValues)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Description = description;
            this.ContextTypeName = contextTypeName;
            this.ContextDescription = contextDescription;
            this.Parameters = parameters is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ParameterDescriptor>(parameters);
            this.ReturnValues = returnValues is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ReturnValueDescriptor>(returnValues);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.AggregatorDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.AggregatorDescriptorBuilder(this);
        }
    }
    public partial record Case
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Evaluatable Condition
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Expression Expression
        {
            get;
        }

        public Case(ExpressionFramework.Domain.Evaluatable condition, ExpressionFramework.Domain.Expression expression)
        {
            this.Condition = condition;
            this.Expression = expression;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.CaseBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.CaseBuilder(this);
        }
    }
    public partial record EvaluatableDescriptor
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string TypeName
        {
            get;
        }

        public string Description
        {
            get;
        }

        public bool UsesContext
        {
            get;
        }

        public string? ContextTypeName
        {
            get;
        }

        public string? ContextDescription
        {
            get;
        }

        public System.Nullable<bool> ContextIsRequired
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ParameterDescriptor> Parameters
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ReturnValueDescriptor> ReturnValues
        {
            get;
        }

        public EvaluatableDescriptor(string name, string typeName, string description, bool usesContext, string? contextTypeName, string? contextDescription, System.Nullable<bool> contextIsRequired, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ParameterDescriptor> parameters, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ReturnValueDescriptor> returnValues)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Description = description;
            this.UsesContext = usesContext;
            this.ContextTypeName = contextTypeName;
            this.ContextDescription = contextDescription;
            this.ContextIsRequired = contextIsRequired;
            this.Parameters = parameters is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ParameterDescriptor>(parameters);
            this.ReturnValues = returnValues is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ReturnValueDescriptor>(returnValues);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.EvaluatableDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.EvaluatableDescriptorBuilder(this);
        }
    }
    public partial record ExpressionDescriptor
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string TypeName
        {
            get;
        }

        public string Description
        {
            get;
        }

        public bool UsesContext
        {
            get;
        }

        public string? ContextTypeName
        {
            get;
        }

        public string? ContextDescription
        {
            get;
        }

        public System.Nullable<bool> ContextIsRequired
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ParameterDescriptor> Parameters
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ReturnValueDescriptor> ReturnValues
        {
            get;
        }

        public ExpressionDescriptor(string name, string typeName, string description, bool usesContext, string? contextTypeName, string? contextDescription, System.Nullable<bool> contextIsRequired, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ParameterDescriptor> parameters, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ReturnValueDescriptor> returnValues)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Description = description;
            this.UsesContext = usesContext;
            this.ContextTypeName = contextTypeName;
            this.ContextDescription = contextDescription;
            this.ContextIsRequired = contextIsRequired;
            this.Parameters = parameters is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ParameterDescriptor>(parameters);
            this.ReturnValues = returnValues is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ReturnValueDescriptor>(returnValues);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.ExpressionDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.ExpressionDescriptorBuilder(this);
        }
    }
    public partial record OperatorDescriptor
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string TypeName
        {
            get;
        }

        public string Description
        {
            get;
        }

        public bool UsesLeftValue
        {
            get;
        }

        public string? LeftValueTypeName
        {
            get;
        }

        public bool UsesRightValue
        {
            get;
        }

        public string? RightValueTypeName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ParameterDescriptor> Parameters
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Domain.ReturnValueDescriptor> ReturnValues
        {
            get;
        }

        public OperatorDescriptor(string name, string typeName, string description, bool usesLeftValue, string? leftValueTypeName, bool usesRightValue, string? rightValueTypeName, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ParameterDescriptor> parameters, System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.ReturnValueDescriptor> returnValues)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Description = description;
            this.UsesLeftValue = usesLeftValue;
            this.LeftValueTypeName = leftValueTypeName;
            this.UsesRightValue = usesRightValue;
            this.RightValueTypeName = rightValueTypeName;
            this.Parameters = parameters is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ParameterDescriptor>(parameters);
            this.ReturnValues = returnValues is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ExpressionFramework.Domain.ReturnValueDescriptor>(returnValues);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.OperatorDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.OperatorDescriptorBuilder(this);
        }
    }
    public partial record ParameterDescriptor
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string TypeName
        {
            get;
        }

        public string Description
        {
            get;
        }

        public bool Required
        {
            get;
        }

        public ParameterDescriptor(string name, string typeName, string description, bool required)
        {
            this.Name = name;
            this.TypeName = typeName;
            this.Description = description;
            this.Required = required;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.ParameterDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.ParameterDescriptorBuilder(this);
        }
    }
    public partial record ReturnValueDescriptor
    {
        public CrossCutting.Common.Results.ResultStatus Status
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Value
        {
            get;
        }

        public System.Type? ValueType
        {
            get;
        }

        public string Description
        {
            get;
        }

        public ReturnValueDescriptor(CrossCutting.Common.Results.ResultStatus status, string value, System.Type? valueType, string description)
        {
            this.Status = status;
            this.Value = value;
            this.ValueType = valueType;
            this.Description = description;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.ReturnValueDescriptorBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.ReturnValueDescriptorBuilder(this);
        }
    }
    public partial record SortOrder
    {
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Expression SortExpression
        {
            get;
        }

        public ExpressionFramework.Domain.Domains.SortOrderDirection Direction
        {
            get;
        }

        public SortOrder(ExpressionFramework.Domain.Expression sortExpression, ExpressionFramework.Domain.Domains.SortOrderDirection direction)
        {
            this.SortExpression = sortExpression;
            this.Direction = direction;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public ExpressionFramework.Domain.Builders.SortOrderBuilder ToBuilder()
        {
            return new ExpressionFramework.Domain.Builders.SortOrderBuilder(this);
        }
    }
}
#nullable disable
