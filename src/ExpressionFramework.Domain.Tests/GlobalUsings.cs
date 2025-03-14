﻿global using System.Collections;
global using System.ComponentModel.DataAnnotations;
global using System.Globalization;
global using System.Reflection;
global using AutoFixture;
global using AutoFixture.AutoNSubstitute;
global using AutoFixture.Kernel;
global using CrossCutting.Common;
global using CrossCutting.Common.Abstractions;
global using CrossCutting.Common.Extensions;
global using CrossCutting.Common.Results;
global using CsharpExpressionDumper.Abstractions;
global using CsharpExpressionDumper.Core.Extensions;
global using ExpressionFramework.Domain.AggregatorDescriptors;
global using ExpressionFramework.Domain.Aggregators;
global using ExpressionFramework.Domain.Attributes;
global using ExpressionFramework.Domain.Builders;
global using ExpressionFramework.Domain.Builders.Aggregators;
global using ExpressionFramework.Domain.Builders.Evaluatables;
global using ExpressionFramework.Domain.Builders.Expressions;
global using ExpressionFramework.Domain.Builders.Operators;
global using ExpressionFramework.Domain.Contracts;
global using ExpressionFramework.Domain.Domains;
global using ExpressionFramework.Domain.EvaluatableDescriptorProviders;
global using ExpressionFramework.Domain.Evaluatables;
global using ExpressionFramework.Domain.ExpressionDescriptorProviders;
global using ExpressionFramework.Domain.Expressions;
global using ExpressionFramework.Domain.OperatorDescriptorProviders;
global using ExpressionFramework.Domain.Operators;
global using ExpressionFramework.SpecFlow.Tests.Support;
global using Microsoft.Extensions.DependencyInjection;
global using NSubstitute;
global using Reqnroll;
global using Reqnroll.Assist;
global using Shouldly;
global using Xunit;
