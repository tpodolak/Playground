﻿using System.Collections.Generic;
using System.Linq;
using DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Templating.Models;
using Microsoft.AspNetCore.Html;
using NSwag;
using NSwag.CodeGeneration.CSharp.Models;
using RazorLight;
using RazorLight.Text;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Extensions
{
    public static class TemplatePageExtensions
    {
        private static readonly string GeneratorName = typeof(TemplatePageExtensions).Assembly.GetName().Name;

        public static HtmlString FileHeader(this TemplatePage myHelper)
        {
            return new HtmlString($@"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ${GeneratorName}
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------");
        }

        public static IRawString CreateMethodArgumentsWithoutSessionToken(this TemplatePage<DotRezClientTemplateModel> templatePage,
            CSharpOperationModel operation)
        {
            return CreateMethodArguments(templatePage,
                operation.Parameters.Where(parameter => parameter.IsXSessionTokenParameter() == false).ToList());
        }

        public static IRawString CreateMethodArguments(this TemplatePage<DotRezClientTemplateModel> templatePage, CSharpOperationModel operation)
        {
            return CreateMethodArguments(templatePage, operation.Parameters);
        }

        public static IRawString CreateMethodArguments(this TemplatePage<DotRezClientTemplateModel> templatePage,
            IList<CSharpParameterModel> parameters)
        {
            var cSharpParameterModels = parameters.Select(parameter => $"{parameter.Type} {parameter.VariableName}");
            return templatePage.Raw(string.Join(", ", cSharpParameterModels));
        }

        public static IRawString CreateMethodArguments(this TemplatePage<DotRezClientTemplateModel> templatePage, MethodOverloadDefinition operation)
        {
            return templatePage.CreateMethodArguments(operation.Parameters);
        }

        public static IRawString CreateMethodParameters(this TemplatePage<DotRezClientTemplateModel> templatePage, CSharpOperationModel operation)
        {
            return templatePage.CreateMethodParameters(operation.Parameters);
        }

        public static IRawString CreateMethodParameters(this TemplatePage<DotRezClientTemplateModel> templatePage,
            MethodOverloadDefinition overloadDefinition)
        {
            var descriptorMap = overloadDefinition.Descriptors.ToDictionary(descriptor => descriptor.VariableName);
            var cSharpParameterModels = overloadDefinition.Operation.Parameters.Select(parameter =>
                descriptorMap.TryGetValue(parameter.VariableName, out var descriptor) ? descriptor.Substitute : $"{parameter.VariableName}");
            return templatePage.Raw(string.Join(", ", cSharpParameterModels));
        }

        public static IRawString RenderEncodeParameterSegment(this TemplatePage<DotRezClientTemplateModel> templatePage,
            CSharpParameterModel parameterModel)
        {
            if (parameterModel.IsDateArray)
            {
                return templatePage.Raw(
                    $@"System.Uri.EscapeDataString(string.Join("","", System.Linq.Enumerable.Select({
                            parameterModel.VariableName
                        }, item => item.ToString(""{
                            templatePage.Model.ParameterDateTimeFormat
                        }"", System.Globalization.CultureInfo.InvariantCulture))))");
            }

            if (parameterModel.IsDate)
            {
                return templatePage.Raw($@"System.Uri.EscapeDataString({parameterModel.VariableName}.ToString(""{
                        templatePage.Model.ParameterDateTimeFormat
                    }"", System.Globalization.CultureInfo.InvariantCulture))");
            }

            if (parameterModel.IsArray)
            {
                return templatePage.Raw(
                    $@"System.Uri.EscapeDataString(string.Join("","", System.Linq.Enumerable.Select({
                            parameterModel.VariableName
                        }, item => ConvertToString(item, System.Globalization.CultureInfo.InvariantCulture))))");
            }

            return templatePage.Raw(
                $@"System.Uri.EscapeDataString(ConvertToString({parameterModel.VariableName}, System.Globalization.CultureInfo.InvariantCulture))");
        }

        public static IRawString CreateMethodParameters(this TemplatePage<DotRezClientTemplateModel> templatePage,
            IList<CSharpParameterModel> parameters)
        {
            var cSharpParameterModels = parameters.Select(parameter => $"{parameter.VariableName}");
            return templatePage.Raw(string.Join(", ", cSharpParameterModels));
        }

        private static IRawString CreateMethodArguments(TemplatePage templatePage, IList<CSharpParameterModel> parameters)
        {
            var cSharpParameterModels = parameters.Select(parameter => $"{parameter.Type} {parameter.VariableName}");
            return templatePage.Raw(string.Join(", ", cSharpParameterModels));
        }
    }
}