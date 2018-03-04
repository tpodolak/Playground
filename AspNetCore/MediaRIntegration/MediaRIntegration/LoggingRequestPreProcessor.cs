using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MediaRIntegration
{
    public class LoggingRequestPreProcessor<T> : IRequestPreProcessor<T>
    {
        private readonly ILogger<LoggingRequestPreProcessor<T>> _logger;

        public LoggingRequestPreProcessor(ILogger<LoggingRequestPreProcessor<T>> logger)
        {
            _logger = logger;
        }

        public Task Process(T request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"About to process request {request.GetType().Name}");
            return Task.CompletedTask;
        }
    }
}