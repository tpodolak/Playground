using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotRezCore.Api.OperationFilters
{
    public class AddTokenHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            if (operation.Parameters.Any(parameter => parameter.Name == Constants.Session.XSessionTokenHeaderName) == false)
            {
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = Constants.Session.XSessionTokenHeaderName,
                    In = "header",
                    Type = "string",
                    Required = true
                });   
            }
        }
    }
}