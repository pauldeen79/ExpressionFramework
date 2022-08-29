﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 6.0.8
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.CodeGenerationProviders
{
#nullable enable
    public partial class ExpressionFrameworkCSharpClassBase
    {
        protected static ITypeBase[] GetBaseModels()
        {
            return new[]
            {
                new InterfaceBuilder()
                    .WithNamespace(@"ExpressionFramework.Abstractions.DomainModel")
                    .AddProperties(
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"LeftExpression")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.IExpression")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"),
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"Operator")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.Domains.Operator")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"),
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"RightExpression")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.IExpression")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"),
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"StartGroup")
                            .WithTypeName(@"System.Boolean")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"),
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"EndGroup")
                            .WithTypeName(@"System.Boolean")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"),
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"Combination")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.Domains.Combination")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.ICondition"))
                    .WithName(@"ICondition"),
                new InterfaceBuilder()
                    .WithNamespace(@"ExpressionFramework.Abstractions.DomainModel")
                    .AddProperties(
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"Function")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.IExpressionFunction")
                            .WithIsNullable(true)
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.IExpression"))
                    .AddMethods(
                        new ClassMethodBuilder()
                            .WithVirtual(true)
                            .WithAbstract(true)
                            .WithName(@"ToBuilder")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.Builders.IExpressionBuilder")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.IExpression"))
                    .WithName(@"IExpression"),
                new InterfaceBuilder()
                    .WithNamespace(@"ExpressionFramework.Abstractions.DomainModel")
                    .AddProperties(
                        new ClassPropertyBuilder()
                            .WithHasSetter(false)
                            .WithName(@"InnerFunction")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.IExpressionFunction")
                            .WithIsNullable(true)
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.IExpressionFunction"))
                    .AddMethods(
                        new ClassMethodBuilder()
                            .WithVirtual(true)
                            .WithAbstract(true)
                            .WithName(@"ToBuilder")
                            .WithTypeName(@"ExpressionFramework.Abstractions.DomainModel.Builders.IExpressionFunctionBuilder")
                            .WithParentTypeFullName(@"ExpressionFramework.Abstractions.DomainModel.IExpressionFunction"))
                    .WithName(@"IExpressionFunction"),
            }.Select(x => x.Build()).ToArray();
        }
    }
#nullable restore
}
