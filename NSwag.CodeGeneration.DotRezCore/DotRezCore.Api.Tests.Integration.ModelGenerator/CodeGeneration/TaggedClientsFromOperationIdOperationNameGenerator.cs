using System;
using System.Collections.Generic;
using System.Linq;
using NJsonSchema;
using NSwag;
using NSwag.CodeGeneration.OperationNameGenerators;

namespace DotRezCore.Api.Tests.Integration.ModelGenerator.CodeGeneration
{
    public class TaggedClientsFromOperationIdOperationNameGenerator : IOperationNameGenerator
    {
        public bool SupportsMultipleClients { get; } = true;

        public string GetClientName(SwaggerDocument document, string path, SwaggerOperationMethod httpMethod, SwaggerOperation operation)
        {
            return $"{operation.Tags.First()}";
        }

        /// <summary>Gets the operation name for a given operation.</summary>
        /// <param name="document">The Swagger document.</param>
        /// <param name="path">The HTTP path.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="operation">The operation.</param>
        /// <returns>The operation name.</returns>
        public string GetOperationName(SwaggerDocument document, string path, SwaggerOperationMethod httpMethod, SwaggerOperation operation)
        {
            var clientName = GetClientName(document, path, httpMethod, operation);
            var operationName = operation.OperationId.Replace($"Api{clientName}", string.Empty);
            return operationName;
        }
    }
}