﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 9.0.2
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
#nullable enable
namespace ExpressionFramework.Domain.Builders
{
    public abstract partial class AggregatorBuilder<TBuilder, TEntity> : AggregatorBuilder
        where TEntity : ExpressionFramework.Domain.Aggregator
        where TBuilder : AggregatorBuilder<TBuilder, TEntity>
    {
        protected AggregatorBuilder(ExpressionFramework.Domain.Aggregator source) : base(source)
        {
        }

        protected AggregatorBuilder() : base()
        {
        }

        public override ExpressionFramework.Domain.Aggregator Build()
        {
            return BuildTyped();
        }

        public abstract TEntity BuildTyped();

        public static implicit operator ExpressionFramework.Domain.Aggregator(AggregatorBuilder<TBuilder, TEntity> entity)
        {
            return entity.BuildTyped();
        }
    }
    public abstract partial class EvaluatableBuilder<TBuilder, TEntity> : EvaluatableBuilder
        where TEntity : ExpressionFramework.Domain.Evaluatable
        where TBuilder : EvaluatableBuilder<TBuilder, TEntity>
    {
        protected EvaluatableBuilder(ExpressionFramework.Domain.Evaluatable source) : base(source)
        {
        }

        protected EvaluatableBuilder() : base()
        {
        }

        public override ExpressionFramework.Domain.Evaluatable Build()
        {
            return BuildTyped();
        }

        public abstract TEntity BuildTyped();

        public static implicit operator ExpressionFramework.Domain.Evaluatable(EvaluatableBuilder<TBuilder, TEntity> entity)
        {
            return entity.BuildTyped();
        }
    }
    public abstract partial class ExpressionBuilder<TBuilder, TEntity> : ExpressionBuilder
        where TEntity : ExpressionFramework.Domain.Expression
        where TBuilder : ExpressionBuilder<TBuilder, TEntity>
    {
        protected ExpressionBuilder(ExpressionFramework.Domain.Expression source) : base(source)
        {
        }

        protected ExpressionBuilder() : base()
        {
        }

        public override ExpressionFramework.Domain.Expression Build()
        {
            return BuildTyped();
        }

        public abstract TEntity BuildTyped();

        public static implicit operator ExpressionFramework.Domain.Expression(ExpressionBuilder<TBuilder, TEntity> entity)
        {
            return entity.BuildTyped();
        }
    }
    public abstract partial class OperatorBuilder<TBuilder, TEntity> : OperatorBuilder
        where TEntity : ExpressionFramework.Domain.Operator
        where TBuilder : OperatorBuilder<TBuilder, TEntity>
    {
        protected OperatorBuilder(ExpressionFramework.Domain.Operator source) : base(source)
        {
        }

        protected OperatorBuilder() : base()
        {
        }

        public override ExpressionFramework.Domain.Operator Build()
        {
            return BuildTyped();
        }

        public abstract TEntity BuildTyped();

        public static implicit operator ExpressionFramework.Domain.Operator(OperatorBuilder<TBuilder, TEntity> entity)
        {
            return entity.BuildTyped();
        }
    }
}
#nullable disable
