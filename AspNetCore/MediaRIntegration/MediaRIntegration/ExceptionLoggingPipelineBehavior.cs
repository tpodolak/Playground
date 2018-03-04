using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediaRIntegration
{
    public class ExceptionLoggingPipelineBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<ExceptionLoggingPipelineBehavior<TRequest, TResponse>> _logger;

        public ExceptionLoggingPipelineBehavior(ILogger<ExceptionLoggingPipelineBehavior<TRequest,TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.HResult, exception, exception.Message);
                throw;
            }
        }
    }
}