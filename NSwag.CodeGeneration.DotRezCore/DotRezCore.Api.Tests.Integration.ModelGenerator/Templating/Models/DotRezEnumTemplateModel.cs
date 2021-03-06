﻿using System.Linq;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;
using NJsonSchema.CodeGeneration.CSharp.Models;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Templating.Models
{
    public class DotRezEnumTemplateModel : EnumTemplateModel
    {
        private readonly string _typeName;

        public DotRezEnumTemplateModel(string typeName, JsonSchema4 schema, CSharpGeneratorSettings settings) : base(typeName, schema, settings)
        {
            _typeName = typeName;
        }

        public string Namespace
        {
            get
            {
                var split = _typeName.Split('.');
                return string.Join(".", split.Take(split.Length - 1));
            }
        }

        public new string Name
        {
            get
            {
                var split = _typeName.Split('.');
                return split.Last();
            }
        }
    }
}