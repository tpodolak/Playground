using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.Swagger
{
    public class SchemaRetriever : ISchemaRetriever
    {        
        public List<string> RetrieveSchemas(IWebHost webhost)
        {
            var serviceProvider = webhost.Services;
            
            var swaggerProvider = serviceProvider.GetRequiredService<ISwaggerProvider>();
            var mvcJsonOptions = serviceProvider.GetRequiredService<IOptions<MvcJsonOptions>>();

            var serializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = mvcJsonOptions.Value.SerializerSettings.Formatting,
                ContractResolver = new SwaggerContractResolver(mvcJsonOptions.Value.SerializerSettings)
            };

            return GetDocumentIds(swaggerProvider).Select(documentId =>
            {
                var document = swaggerProvider.GetSwagger(documentId, null, "/");
                using (var stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, document);
                    return stringWriter.ToString();
                }

                ;
            }).ToList();
        }

        private IEnumerable<string> GetDocumentIds(ISwaggerProvider swaggerProvider)
        {
            var settings = (SwaggerGeneratorSettings)swaggerProvider.GetType().GetField("_settings", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(swaggerProvider);
            return settings.SwaggerDocs.Keys;
        }
    }
}