using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger.Interfaces
{
    public interface ISchemaRetriever
    {
        List<string> RetrieveSchemas(IWebHost webhost);
    }
}