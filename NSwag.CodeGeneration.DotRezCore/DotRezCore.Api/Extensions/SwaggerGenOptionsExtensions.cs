﻿using DotRezCore.Api.OperationFilters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotRezCore.Api.Extensions
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void UseCodeGeneratorFilters(this SwaggerGenOptions options)
        {
            options.OperationFilter<UseOpenApi3OperationId>();
        }
    }
}