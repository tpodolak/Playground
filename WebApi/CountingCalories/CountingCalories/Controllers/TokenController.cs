using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Data.Entities;
using CountingCalories.Infrastructure;
using CountingCalories.Models;

namespace CountingCalories.Controllers
{
    public class TokenController : ApiController
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly IModelFactory modelFactory;

        public TokenController(ICountingCaloriesRepository repo, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.modelFactory = modelFactory;
        }

        /// <summary>
        /// working sample values values
        /// ApiKey: SSB3aWxsIG1ha2UgbXkgQVBJIHNlY3VyZQ==
        /// Secret: CPcY0x9TUjbZmf1Fgs4DH+9zU2nEKcZIrgs/cnPUJD0=
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] TokenRequestModel model)
        {
            try
            {
                var apiUser = repo.GetApiUsers().FirstOrDefault(val => val.AppId == model.ApiKey);
                if (apiUser != null)
                {
                    var secret = apiUser.Secret;
                    // Simplistic implementation DO NOT USE IN REAL APP
                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);
                    // Compute Hash from API Key (NOT SECURE)
                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(apiUser.AppId));
                    var signature = Convert.ToBase64String(hash);
                    if (signature == model.Secret)
                    {
                        var rawTokenInfo = string.Concat(apiUser.AppId, DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        // simplyfied solution one token at time
                        var authToken = new AuthToken()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.UtcNow.AddDays(7),
                            ApiUser = apiUser
                        };
                        if (repo.Insert(authToken) && repo.SaveAll())
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, modelFactory.Create(authToken));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}