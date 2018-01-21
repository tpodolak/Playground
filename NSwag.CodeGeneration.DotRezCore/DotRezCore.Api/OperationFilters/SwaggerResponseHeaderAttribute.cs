using System;

namespace DotRezCore.Api.OperationFilters
{
    public class SwaggerResponseHeaderAttribute : Attribute
    {
        public int StatusCode { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Type { get; set; }
    }
}