using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace CountingCalories.Infrastructure.Services
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var metadata = new ErrorData
            {
                Message = "An unexpected error occurred! Please use the ticket ID to contact support",
                DateTime = DateTime.UtcNow,
                RequestUri = context.Request.RequestUri,
                ErrorId = Guid.NewGuid()
            };

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, metadata);
            context.Result = new ResponseMessageResult(response);
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        private class ErrorData
        {
            public string Message { get; set; }
            public DateTime DateTime { get; set; }
            public Uri RequestUri { get; set; }
            public Guid ErrorId { get; set; }
        }
    }
}