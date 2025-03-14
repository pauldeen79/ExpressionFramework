﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 9.0.3
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
#nullable enable
namespace ExpressionFramework.Core.Builders.Evaluatables
{
    public partial class ComposableEvaluatableBuilder : ExpressionFramework.Core.Builders.EvaluatableBaseBuilder<ComposableEvaluatableBuilder, ExpressionFramework.Core.Evaluatables.ComposableEvaluatable>, ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder
    {
        private ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder _innerEvaluatable;

        private System.Nullable<ExpressionFramework.Core.Domains.Combination> _combination;

        private bool _startGroup;

        private bool _endGroup;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder InnerEvaluatable
        {
            get
            {
                return _innerEvaluatable;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder>.Default.Equals(_innerEvaluatable!, value!);
                _innerEvaluatable = value ?? throw new System.ArgumentNullException(nameof(value));
                if (hasChanged) HandlePropertyChanged(nameof(InnerEvaluatable));
            }
        }

        public System.Nullable<ExpressionFramework.Core.Domains.Combination> Combination
        {
            get
            {
                return _combination;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Nullable<ExpressionFramework.Core.Domains.Combination>>.Default.Equals(_combination, value);
                _combination = value;
                if (hasChanged) HandlePropertyChanged(nameof(Combination));
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
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Boolean>.Default.Equals(_startGroup, value);
                _startGroup = value;
                if (hasChanged) HandlePropertyChanged(nameof(StartGroup));
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
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Boolean>.Default.Equals(_endGroup, value);
                _endGroup = value;
                if (hasChanged) HandlePropertyChanged(nameof(EndGroup));
            }
        }

        public ComposableEvaluatableBuilder(ExpressionFramework.Core.Evaluatables.ComposableEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _innerEvaluatable = source.InnerEvaluatable?.ToBuilder()!;
            _combination = source.Combination;
            _startGroup = source.StartGroup;
            _endGroup = source.EndGroup;
        }

        public ComposableEvaluatableBuilder() : base()
        {
            _innerEvaluatable = default(ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Core.Evaluatables.ComposableEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Core.Evaluatables.ComposableEvaluatable(InnerEvaluatable?.Build()!, Combination, StartGroup, EndGroup);
        }

        ExpressionFramework.Core.Abstractions.IEvaluatable ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder.Build()
        {
            return BuildTyped();
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder WithInnerEvaluatable(ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder innerEvaluatable)
        {
            if (innerEvaluatable is null) throw new System.ArgumentNullException(nameof(innerEvaluatable));
            InnerEvaluatable = innerEvaluatable;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder WithCombination(System.Nullable<ExpressionFramework.Core.Domains.Combination> combination)
        {
            Combination = combination;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder WithStartGroup(bool startGroup = true)
        {
            StartGroup = startGroup;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder WithEndGroup(bool endGroup = true)
        {
            EndGroup = endGroup;
            return this;
        }

        public static implicit operator ExpressionFramework.Core.Evaluatables.ComposableEvaluatable(ComposableEvaluatableBuilder entity)
        {
            return entity.BuildTyped();
        }
    }
    public partial class ComposedEvaluatableBuilder : ExpressionFramework.Core.Builders.EvaluatableBaseBuilder<ComposedEvaluatableBuilder, ExpressionFramework.Core.Evaluatables.ComposedEvaluatable>, ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder
    {
        private System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder> _conditions;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder> Conditions
        {
            get
            {
                return _conditions;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Collections.Generic.IReadOnlyCollection<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder>>.Default.Equals(_conditions!, value!);
                _conditions = value ?? throw new System.ArgumentNullException(nameof(value));
                if (hasChanged) HandlePropertyChanged(nameof(Conditions));
            }
        }

        public ComposedEvaluatableBuilder(ExpressionFramework.Core.Evaluatables.ComposedEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _conditions = new System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder>();
            if (source.Conditions is not null) foreach (var item in source.Conditions.Select(x => new ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder(x))) _conditions.Add(item);
        }

        public ComposedEvaluatableBuilder() : base()
        {
            _conditions = new System.Collections.ObjectModel.ObservableCollection<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder>();
            SetDefaultValues();
        }

        public override ExpressionFramework.Core.Evaluatables.ComposedEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Core.Evaluatables.ComposedEvaluatable(Conditions.Select(x => x.BuildTyped()!).ToList().AsReadOnly());
        }

        ExpressionFramework.Core.Abstractions.IEvaluatable ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder.Build()
        {
            return BuildTyped();
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Core.Builders.Evaluatables.ComposedEvaluatableBuilder AddConditions(System.Collections.Generic.IEnumerable<ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder> conditions)
        {
            if (conditions is null) throw new System.ArgumentNullException(nameof(conditions));
            return AddConditions(conditions.ToArray());
        }

        public ExpressionFramework.Core.Builders.Evaluatables.ComposedEvaluatableBuilder AddConditions(params ExpressionFramework.Core.Builders.Evaluatables.ComposableEvaluatableBuilder[] conditions)
        {
            if (conditions is null) throw new System.ArgumentNullException(nameof(conditions));
            foreach (var item in conditions) Conditions.Add(item);
            return this;
        }

        public static implicit operator ExpressionFramework.Core.Evaluatables.ComposedEvaluatable(ComposedEvaluatableBuilder entity)
        {
            return entity.BuildTyped();
        }
    }
    public partial class ConstantEvaluatableBuilder : ExpressionFramework.Core.Builders.EvaluatableBaseBuilder<ConstantEvaluatableBuilder, ExpressionFramework.Core.Evaluatables.ConstantEvaluatable>, ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder
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
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Boolean>.Default.Equals(_value, value);
                _value = value;
                if (hasChanged) HandlePropertyChanged(nameof(Value));
            }
        }

        public ConstantEvaluatableBuilder(ExpressionFramework.Core.Evaluatables.ConstantEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _value = source.Value;
        }

        public ConstantEvaluatableBuilder() : base()
        {
            SetDefaultValues();
        }

        public override ExpressionFramework.Core.Evaluatables.ConstantEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Core.Evaluatables.ConstantEvaluatable(Value);
        }

        ExpressionFramework.Core.Abstractions.IEvaluatable ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder.Build()
        {
            return BuildTyped();
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Core.Builders.Evaluatables.ConstantEvaluatableBuilder WithValue(bool value = true)
        {
            Value = value;
            return this;
        }

        public static implicit operator ExpressionFramework.Core.Evaluatables.ConstantEvaluatable(ConstantEvaluatableBuilder entity)
        {
            return entity.BuildTyped();
        }
    }
    public partial class DelegateEvaluatableBuilder : ExpressionFramework.Core.Builders.EvaluatableBaseBuilder<DelegateEvaluatableBuilder, ExpressionFramework.Core.Evaluatables.DelegateEvaluatable>, ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder
    {
        private System.Func<bool> _delegate;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Func<bool> Delegate
        {
            get
            {
                return _delegate;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Func<System.Boolean>>.Default.Equals(_delegate!, value!);
                _delegate = value ?? throw new System.ArgumentNullException(nameof(value));
                if (hasChanged) HandlePropertyChanged(nameof(Delegate));
            }
        }

        public DelegateEvaluatableBuilder(ExpressionFramework.Core.Evaluatables.DelegateEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _delegate = source.Delegate;
        }

        public DelegateEvaluatableBuilder() : base()
        {
            _delegate = default(System.Func<System.Boolean>)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Core.Evaluatables.DelegateEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Core.Evaluatables.DelegateEvaluatable(Delegate);
        }

        ExpressionFramework.Core.Abstractions.IEvaluatable ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder.Build()
        {
            return BuildTyped();
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Core.Builders.Evaluatables.DelegateEvaluatableBuilder WithDelegate(System.Func<bool> @delegate)
        {
            if (@delegate is null) throw new System.ArgumentNullException(nameof(@delegate));
            Delegate = @delegate;
            return this;
        }

        public static implicit operator ExpressionFramework.Core.Evaluatables.DelegateEvaluatable(DelegateEvaluatableBuilder entity)
        {
            return entity.BuildTyped();
        }
    }
    public partial class OperatorEvaluatableBuilder : ExpressionFramework.Core.Builders.EvaluatableBaseBuilder<OperatorEvaluatableBuilder, ExpressionFramework.Core.Evaluatables.OperatorEvaluatable>, ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder
    {
        private object? _leftValue;

        private ExpressionFramework.Core.Builders.Abstractions.IOperatorBuilder _operator;

        private object? _rightValue;

        private System.StringComparison _stringComparison;

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public object? LeftValue
        {
            get
            {
                return _leftValue;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Object>.Default.Equals(_leftValue!, value!);
                _leftValue = value;
                if (hasChanged) HandlePropertyChanged(nameof(LeftValue));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public ExpressionFramework.Core.Builders.Abstractions.IOperatorBuilder Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<ExpressionFramework.Core.Builders.Abstractions.IOperatorBuilder>.Default.Equals(_operator!, value!);
                _operator = value ?? throw new System.ArgumentNullException(nameof(value));
                if (hasChanged) HandlePropertyChanged(nameof(Operator));
            }
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public object? RightValue
        {
            get
            {
                return _rightValue;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.Object>.Default.Equals(_rightValue!, value!);
                _rightValue = value;
                if (hasChanged) HandlePropertyChanged(nameof(RightValue));
            }
        }

        public System.StringComparison StringComparison
        {
            get
            {
                return _stringComparison;
            }
            set
            {
                bool hasChanged = !System.Collections.Generic.EqualityComparer<System.StringComparison>.Default.Equals(_stringComparison, value);
                _stringComparison = value;
                if (hasChanged) HandlePropertyChanged(nameof(StringComparison));
            }
        }

        public OperatorEvaluatableBuilder(ExpressionFramework.Core.Evaluatables.OperatorEvaluatable source) : base(source)
        {
            if (source is null) throw new System.ArgumentNullException(nameof(source));
            _leftValue = source.LeftValue;
            _operator = source.Operator?.ToBuilder()!;
            _rightValue = source.RightValue;
            _stringComparison = source.StringComparison;
        }

        public OperatorEvaluatableBuilder() : base()
        {
            _operator = default(ExpressionFramework.Core.Builders.Abstractions.IOperatorBuilder)!;
            SetDefaultValues();
        }

        public override ExpressionFramework.Core.Evaluatables.OperatorEvaluatable BuildTyped()
        {
            return new ExpressionFramework.Core.Evaluatables.OperatorEvaluatable(LeftValue, Operator?.Build()!, RightValue, StringComparison);
        }

        ExpressionFramework.Core.Abstractions.IEvaluatable ExpressionFramework.Core.Builders.Abstractions.IEvaluatableBuilder.Build()
        {
            return BuildTyped();
        }

        partial void SetDefaultValues();

        public ExpressionFramework.Core.Builders.Evaluatables.OperatorEvaluatableBuilder WithLeftValue(object? leftValue)
        {
            LeftValue = leftValue;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.OperatorEvaluatableBuilder WithOperator(ExpressionFramework.Core.Builders.Abstractions.IOperatorBuilder @operator)
        {
            if (@operator is null) throw new System.ArgumentNullException(nameof(@operator));
            Operator = @operator;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.OperatorEvaluatableBuilder WithRightValue(object? rightValue)
        {
            RightValue = rightValue;
            return this;
        }

        public ExpressionFramework.Core.Builders.Evaluatables.OperatorEvaluatableBuilder WithStringComparison(System.StringComparison stringComparison)
        {
            StringComparison = stringComparison;
            return this;
        }

        public static implicit operator ExpressionFramework.Core.Evaluatables.OperatorEvaluatable(OperatorEvaluatableBuilder entity)
        {
            return entity.BuildTyped();
        }
    }
}
#nullable disable
