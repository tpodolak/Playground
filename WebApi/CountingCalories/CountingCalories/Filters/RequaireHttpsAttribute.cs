using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace CountingKs.Filters
{
    public class RequireHttpsAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var req = actionContext.Request;

            if (req.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                var html = "<p>Https is required</p>";
                if (req.Method.Method == "GET")
                {
                    // tell the caller that we found url but...
                    actionContext.Response = req.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                    var uriBuilder = new UriBuilder(req.RequestUri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = 443
                    };
                    // you should use https so redirect to https
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = req.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
            }
        }
    }
}