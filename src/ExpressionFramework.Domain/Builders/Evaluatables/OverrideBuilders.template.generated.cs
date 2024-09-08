﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.8
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
namespace ExpressionFramework.Domain.Builders.Evaluatables
{
    public partial class ComposableEvaluatableBuilder : ExpressionFramework.Domain.Builders.EvaluatableBuilder<ComposableEvaluatableBuilder, ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable>
    {
        private ExpressionFramework.Domain.Builders.ExpressionBuilder _leftExpression;

        private ExpressionFramework.Domain.Builders.OperatorBuilder _operator;

        private ExpressionFramework.Domain.Builders.ExpressionBuilder _rightExpression;

        private System.Nullable<ExpressionFramework.Domain.Domains.Combination> _combination;

        private bool _startGroup;

        private bool _endGroup;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.ExpressionBuilder LeftExpression
        {
            get
            {
                return _leftExpression;
            }
            set
            {
                _leftExpression = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(LeftExpression));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.OperatorBuilder Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(Operator));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.ExpressionBuilder RightExpression
        {
            get
            {
                return _rightExpression;
            }
            set
            {
                _rightExpression = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(RightExpression));
            }
        }

        public System.Nullable<ExpressionFramework.Domain.Domains.Combination> Combination
        {
            get
            {
                return _combination;
            }
            set
            {
                _combination = value;
                HandlePropertyChanged(nameof(Combination));
            }
        }

        public bool StartGroup
        {
            get
            {
                return _startGroup;
            }
            set
            {
                _startGroup = value;
                HandlePropertyChanged(nameof(StartGroup));
            }
        }

        public bool EndGroup
        {
            get
            {
                return _endGroup;
            }
            set
            {
                _endGroup = value;
                HandlePropertyChanged(nameof(EndGroup));
            }
        }

        public ComposableEvaluatableBuilder(ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _leftExpression = source.LeftExpression?.ToBuilder()!;
            _operator = source.Operator?.ToBuilder()!;
            _rightExpression = source.RightExpression?.ToBuilder()!;
            _combination = source.Combination;
            _startGroup = source.StartGroup;
            _endGroup = source.EndGroup;
        }

        public ComposableEvaluatableBuilder() : base()
        {
            _leftExpression = default(ExpressionFramework.Domain.Builders.ExpressionBuilder)!;
            _operator = default(ExpressionFramework.Domain.Builders.OperatorBuilder)!;
            _rightExpression = default(ExpressionFramework.Domain.Builders.ExpressionBuilder)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Domain.Evaluatables.ComposableEvaluatable(LeftExpression?.Build()!, Operator?.Build()!, RightExpression?.Build()!, Combination, StartGroup, EndGroup);
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithLeftExpression(ExpressionFramework.Domain.Builders.ExpressionBuilder leftExpression)
        {
            if (leftExpression is null) throw new System.ArgumentNullException(nameof(leftExpression));
            LeftExpression = leftExpression;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithOperator(ExpressionFramework.Domain.Builders.OperatorBuilder @operator)
        {
            if (@operator is null) throw new System.ArgumentNullException(nameof(@operator));
            Operator = @operator;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithRightExpression(ExpressionFramework.Domain.Builders.ExpressionBuilder rightExpression)
        {
            if (rightExpression is null) throw new System.ArgumentNullException(nameof(rightExpression));
            RightExpression = rightExpression;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithCombination(System.Nullable<ExpressionFramework.Domain.Domains.Combination> combination)
        {
            Combination = combination;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithStartGroup(bool startGroup = true)
        {
            StartGroup = startGroup;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithEndGroup(bool endGroup = true)
        {
            EndGroup = endGroup;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithLeftExpression(object leftExpression)
        {
            LeftExpression = new ConstantExpressionBuilder().WithValue(leftExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithLeftExpression(System.Func<object?, object> leftExpression)
        {
            LeftExpression = new DelegateExpressionBuilder().WithValue(leftExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithRightExpression(object rightExpression)
        {
            RightExpression = new ConstantExpressionBuilder().WithValue(rightExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder WithRightExpression(System.Func<object?, object> rightExpression)
        {
            RightExpression = new DelegateExpressionBuilder().WithValue(rightExpression);
            return this;
        }
    }
    public partial class ComposedEvaluatableBuilder : ExpressionFramework.Domain.Builders.EvaluatableBuilder<ComposedEvaluatableBuilder, ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable>
    {
        private System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder> _conditions;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                _conditions = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(Conditions));
            }
        }

        public ComposedEvaluatableBuilder(ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _conditions = new System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder>();
            if (source.Conditions is not null) foreach (var item in source.Conditions.Select(x => new ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder(x))) _conditions.Add(item);
        }

        public ComposedEvaluatableBuilder() : base()
        {
            _conditions = new System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder>();
            SetDefaultValues();
        }

        public override ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Domain.Evaluatables.ComposedEvaluatable(Conditions.Select(x => x.BuildTyped()!).ToList().AsReadOnly());
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposedEvaluatableBuilder AddConditions(System.Collections.Generic.IEnumerable<ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder> conditions)
        {
            if (conditions is null) throw new System.ArgumentNullException(nameof(conditions));
            return AddConditions(conditions.ToArray());
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.ComposedEvaluatableBuilder AddConditions(params ExpressionFramework.Domain.Builders.Evaluatables.ComposableEvaluatableBuilder[] conditions)
        {
            if (conditions is null) throw new System.ArgumentNullException(nameof(conditions));
            foreach (var item in conditions) Conditions.Add(item);
            return this;
        }
    }
    public partial class ConstantEvaluatableBuilder : ExpressionFramework.Domain.Builders.EvaluatableBuilder<ConstantEvaluatableBuilder, ExpressionFramework.Domain.Evaluatables.ConstantEvaluatable>
    {
        private bool _value;

        public bool Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                HandlePropertyChanged(nameof(Value));
            }
        }

        public ConstantEvaluatableBuilder(ExpressionFramework.Domain.Evaluatables.ConstantEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _value = source.Value;
        }

        public ConstantEvaluatableBuilder() : base()
        {
            SetDefaultValues();
        }

        public override ExpressionFramework.Domain.Evaluatables.ConstantEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Domain.Evaluatables.ConstantEvaluatable(Value);
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Domain.Builders.Evaluatables.ConstantEvaluatableBuilder WithValue(bool value = true)
        {
            Value = value;
            return this;
        }
    }
    public partial class DelegateEvaluatableBuilder : ExpressionFramework.Domain.Builders.EvaluatableBuilder<DelegateEvaluatableBuilder, ExpressionFramework.Domain.Evaluatables.DelegateEvaluatable>
    {
        private System.Func<bool> _value;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Func<bool> Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(Value));
            }
        }

        public DelegateEvaluatableBuilder(ExpressionFramework.Domain.Evaluatables.DelegateEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _value = source.Value;
        }

        public DelegateEvaluatableBuilder() : base()
        {
            _value = default(System.Func<System.Boolean>)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Domain.Evaluatables.DelegateEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Domain.Evaluatables.DelegateEvaluatable(Value);
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Domain.Builders.Evaluatables.DelegateEvaluatableBuilder WithValue(System.Func<bool> value)
        {
            if (value is null) throw new System.ArgumentNullException(nameof(value));
            Value = value;
            return this;
        }
    }
    public partial class SingleEvaluatableBuilder : ExpressionFramework.Domain.Builders.EvaluatableBuilder<SingleEvaluatableBuilder, ExpressionFramework.Domain.Evaluatables.SingleEvaluatable>
    {
        private ExpressionFramework.Domain.Builders.ExpressionBuilder _leftExpression;

        private ExpressionFramework.Domain.Builders.OperatorBuilder _operator;

        private ExpressionFramework.Domain.Builders.ExpressionBuilder _rightExpression;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.ExpressionBuilder LeftExpression
        {
            get
            {
                return _leftExpression;
            }
            set
            {
                _leftExpression = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(LeftExpression));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.OperatorBuilder Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                _operator = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(Operator));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Domain.Builders.ExpressionBuilder RightExpression
        {
            get
            {
                return _rightExpression;
            }
            set
            {
                _rightExpression = value ?? throw new System.ArgumentNullException(nameof(value));
                HandlePropertyChanged(nameof(RightExpression));
            }
        }

        public SingleEvaluatableBuilder(ExpressionFramework.Domain.Evaluatables.SingleEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _leftExpression = source.LeftExpression?.ToBuilder()!;
            _operator = source.Operator?.ToBuilder()!;
            _rightExpression = source.RightExpression?.ToBuilder()!;
        }

        public SingleEvaluatableBuilder() : base()
        {
            _leftExpression = default(ExpressionFramework.Domain.Builders.ExpressionBuilder)!;
            _operator = default(ExpressionFramework.Domain.Builders.OperatorBuilder)!;
            _rightExpression = default(ExpressionFramework.Domain.Builders.ExpressionBuilder)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Domain.Evaluatables.SingleEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Domain.Evaluatables.SingleEvaluatable(LeftExpression?.Build()!, Operator?.Build()!, RightExpression?.Build()!);
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithLeftExpression(ExpressionFramework.Domain.Builders.ExpressionBuilder leftExpression)
        {
            if (leftExpression is null) throw new System.ArgumentNullException(nameof(leftExpression));
            LeftExpression = leftExpression;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithOperator(ExpressionFramework.Domain.Builders.OperatorBuilder @operator)
        {
            if (@operator is null) throw new System.ArgumentNullException(nameof(@operator));
            Operator = @operator;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithRightExpression(ExpressionFramework.Domain.Builders.ExpressionBuilder rightExpression)
        {
            if (rightExpression is null) throw new System.ArgumentNullException(nameof(rightExpression));
            RightExpression = rightExpression;
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithLeftExpression(object leftExpression)
        {
            LeftExpression = new ConstantExpressionBuilder().WithValue(leftExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithLeftExpression(System.Func<object?, object> leftExpression)
        {
            LeftExpression = new DelegateExpressionBuilder().WithValue(leftExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithRightExpression(object rightExpression)
        {
            RightExpression = new ConstantExpressionBuilder().WithValue(rightExpression);
            return this;
        }

        public ExpressionFramework.Domain.Builders.Evaluatables.SingleEvaluatableBuilder WithRightExpression(System.Func<object?, object> rightExpression)
        {
            RightExpression = new DelegateExpressionBuilder().WithValue(rightExpression);
            return this;
        }
    }
}
#nullable disable
