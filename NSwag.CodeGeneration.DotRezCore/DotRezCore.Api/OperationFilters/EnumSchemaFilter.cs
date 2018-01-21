using System;
using System.Linq;
using DotRezCore.Api.Models.V1;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotRezCore.Api.OperationFilters
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            if (model.Properties == null)
                return;

            var schema2 = context.SchemaRegistry.GetOrRegister(typeof(GetOrderRequest));
            
            var enumProperties = model.Properties.Where(p => p.Value.Enum != null)
                .Union(model.Properties.Where(p => p.Value.Items?.Enum != null)).ToList();
            var enums = context.SystemType.GetProperties()
                .Select(p => Nullable.GetUnderlyingType(p.PropertyType) ?? p.PropertyType.GetElementType() ??
                             p.PropertyType.GetGenericArguments().FirstOrDefault() ?? p.PropertyType)
                .Where(p => p.IsEnum)
                .ToList();

            foreach (var enumProperty in enumProperties)
            {
                var enumPropertyValue = enumProperty.Value.Enum != null ? enumProperty.Value : enumProperty.Value.Items;

                var enumValues = enumPropertyValue.Enum.Select(value => value.ToString()).ToList();
                var enumType = enums.SingleOrDefault(p =>
                {
                    var enumNames = Enum.GetNames(p);
                    if (enumNames.Except(enumValues, StringComparer.InvariantCultureIgnoreCase).Any())
                        return false;
                    if (enumValues.Except(enumNames, StringComparer.InvariantCultureIgnoreCase).Any())
                        return false;
                    return true;
                });

                if (enumType == null)
                    throw new Exception($"Property {enumProperty} not found in {context.SystemType.Name} Type.");

                var schema = context.SchemaRegistry.GetOrRegister(enumType);
                schema.Ref = $"#/definitions/{enumType.FullName}";
                if (context.SchemaRegistry.Definitions.ContainsKey(enumType.FullName) == false)
                {
                    context.SchemaRegistry.Definitions.Add(enumType.FullName, enumPropertyValue);
                }

//                var schema = new Schema
//                {
//                    Ref = $"#/definitions/{enumType.FullName}"
//                };
                if (enumProperty.Value.Enum != null)
                {
                    model.Properties[enumProperty.Key] = schema;
                }
                else if (enumProperty.Value.Items?.Enum != null)
                {
                    enumProperty.Value.Items = schema;
                }
            }
        }
    }
}