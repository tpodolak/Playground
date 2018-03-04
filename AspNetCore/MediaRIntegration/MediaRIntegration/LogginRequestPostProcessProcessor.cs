using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace MediaRIntegration
{
    public class LogginRequestPostProcessProcessor<TRequest,TResponse> : IRequestPostProcessor<TRequest,TResponse>
    {
        private readonly ILogger<LogginRequestPostProcessProcessor<TRequest, TResponse>> _logger;

        public LogginRequestPostProcessProcessor(ILogger<LogginRequestPostProcessProcessor<TRequest,TResponse>> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, TResponse response)
        {
            _logger.LogInformation($"Request {request.GetType().FullName} processed");
            return Task.CompletedTask;
        }
    }
}