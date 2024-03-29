﻿global using System;
global using System.Collections;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using System.Linq;
global using System.Text;
global using CrossCutting.Common;
global using CrossCutting.Common.Abstractions;
global using CrossCutting.Common.Extensions;
global using CrossCutting.Common.Results;
global using CrossCutting.Utilities.Parsers;
global using CrossCutting.Utilities.Parsers.Contracts;
global using ExpressionFramework.CodeGeneration.CodeGenerationProviders;
global using ExpressionFramework.CodeGeneration.Contracts;
global using ExpressionFramework.CodeGeneration.Models;
global using ExpressionFramework.CodeGeneration.Models.Contracts;
global using ExpressionFramework.CodeGeneration.Models.Domains;
global using ExpressionFramework.CodeGeneration.Visitors;
global using Microsoft.Extensions.DependencyInjection;
global using ModelFramework.CodeGeneration.CodeGenerationProviders;
global using ModelFramework.Common;
global using ModelFramework.Common.Extensions;
global using ModelFramework.Objects.Builders;
global using ModelFramework.Objects.Contracts;
global using ModelFramework.Objects.Extensions;
global using ModelFramework.Objects.Settings;
global using TextTemplateTransformationFramework.Runtime;
global using TextTemplateTransformationFramework.Runtime.CodeGeneration;
