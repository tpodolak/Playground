using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CountingCalories.ActionResults
{
    public class VersionedActionResult<T> : IHttpActionResult where T : class
    {
        private readonly HttpRequestMessage requestMessage;
        private readonly string version;
        private readonly T body;

        public VersionedActionResult(HttpRequestMessage requestMessage, string version, T body)
        {
            this.requestMessage = requestMessage;
            this.version = version;
            this.body = body;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // rather synchronous version, in here just to play around with IHttpActionResult;
            var msg = requestMessage.CreateResponse(body);
            msg.Headers.Add("XXX-Version", version);
            return Task.FromResult(msg);
        }
    }
}