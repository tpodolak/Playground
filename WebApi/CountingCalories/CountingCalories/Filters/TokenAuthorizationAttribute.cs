using System.Web.Http.Controllers;

namespace CountingCalories.Filters
{
    // Note this is not OAuth 
    public class TokenAuthorizationAttribute : BasicAuthorizationAttribute
    {
        private readonly bool perUser;
        
        public TokenAuthorizationAttribute(bool perUser = true)
        {
            this.perUser = perUser;
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
#if !DEBUG

            const string apiKeyName = "apikey", tokenName = "token";
            try
            {
                var query = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
                if (!string.IsNullOrWhiteSpace(query[apiKeyName]) || !string.IsNullOrWhiteSpace(query[tokenName]))
                {
                    // TODO figureout Autofac dependency injection for filters
                    var repo = (ICountingCaloriesRepository)actionContext.Request.GetDependencyScope().GetService(typeof (ICountingCaloriesRepository));
                    var apiKey = query[apiKeyName];
                    var token = query[tokenName];
                    var authToken = repo.GetAuthToken(token);
                    if (authToken != null && authToken.ApiUser.AppId == apiKey && authToken.Expiration > DateTime.UtcNow)
                    {
                        if (perUser)
                        {
                            base.OnAuthorization(actionContext);
                        }
                        return;
                    }
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ex);

            }
#endif
        }
    }
}