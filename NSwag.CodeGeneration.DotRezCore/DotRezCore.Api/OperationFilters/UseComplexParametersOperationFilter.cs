using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotRezCore.Api.OperationFilters
{
    public class UseComplexParametersOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var actionDescriptor = ((ControllerActionDescriptor) context.ApiDescription.ActionDescriptor);
            var apiParameters = context.ApiDescription.ParameterDescriptions
                .Where(paramDesc => paramDesc.Source.IsFromRequest)
                .Select(paramDesc => paramDesc)
                .ToList();
            var controllerParameters = actionDescriptor.Parameters;
            // Next if clause discovers operations with complex query parameters
            // because they have more API parameters then method parameters in action
            if (apiParameters.Count != controllerParameters.Count)
            {
                operation.Parameters = controllerParameters
                    .Where(paramDesc => paramDesc.BindingInfo != null)
                    .Where(paramDesc => paramDesc.BindingInfo.BindingSource.IsFromRequest)
                    .Select(paramDesc => CreateParameter(paramDesc, context.SchemaRegistry))
                    .ToList();
                operation.Parameters = (operation.Parameters).Any() ? operation.Parameters : null;
            }
        }

        // Actually the same method used in Swashbuckle with very small difference
        private IParameter CreateParameter(ParameterDescriptor paramDesc, ISchemaRegistry schemaRegistry)
        {
            var source = paramDesc.BindingInfo.BindingSource.Id.ToLower();
            var schema = (paramDesc.ParameterType == null)
                ? null
                : schemaRegistry.GetOrRegister(paramDesc.ParameterType);
            // Actually next line is a difference between default CreateParameter method and this override
            if (source == "body" || (source == "query" && paramDesc.ParameterType.IsClass))
            {
                return new BodyParameter
                {
                    Name = paramDesc.Name,
                    In = source,
                    Schema = schema
                };
            }

            var nonBodyParam = new NonBodyParameter
            {
                Name = paramDesc.Name,
                In = source,
                Required = (source == "path")
            };

            if (schema == null)
                nonBodyParam.Type = "string";
            else
                nonBodyParam.PopulateFrom(schema);

            if (nonBodyParam.Type == "array")
                nonBodyParam.CollectionFormat = "multi";

            return nonBodyParam;
        }
    }

    public static class SwaggerExtensions
    {
        public static void PopulateFrom(this PartialSchema partialSchema, Schema schema)
        {
            if (schema == null) return;

            partialSchema.Type = schema.Type;
            partialSchema.Format = schema.Format;

            if (schema.Items != null)
            {
                // TODO: Handle jagged primitive array and error on jagged object array
                partialSchema.Items = new PartialSchema();
                partialSchema.Items.PopulateFrom(schema.Items);
            }

            partialSchema.Default = schema.Default;
            partialSchema.Maximum = schema.Maximum;
            partialSchema.ExclusiveMaximum = schema.ExclusiveMaximum;
            partialSchema.Minimum = schema.Minimum;
            partialSchema.ExclusiveMinimum = schema.ExclusiveMinimum;
            partialSchema.MaxLength = schema.MaxLength;
            partialSchema.MinLength = schema.MinLength;
            partialSchema.Pattern = schema.Pattern;
            partialSchema.MaxItems = schema.MaxItems;
            partialSchema.MinItems = schema.MinItems;
            partialSchema.UniqueItems = schema.UniqueItems;
            partialSchema.Enum = schema.Enum;
            partialSchema.MultipleOf = schema.MultipleOf;
        }
    }
}